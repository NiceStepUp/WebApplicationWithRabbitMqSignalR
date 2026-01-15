using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Weather.Common.Helpers;
using Weather.Common.Models.Configuration;
using Weather.Common.Models.Weather;
using Weather.Persistence.Abstractions;
using Weather.WebApplicationRabbitMqSignalR.Models.SignalR;
using Weather.WebApplicationRabbitMqSignalR.Services.SignalR.Interfaces;
using Weather.WebApplicationRabbitMqSignalR.SignalR;

namespace Weather.WebApplicationRabbitMqSignalR.Services.SignalR;

/// <summary>
/// Фоновый сервис отслеживания асинхронных операций по погоде
/// </summary>
public class WeatherHostedService : BackgroundService
{
    private readonly IHubContext<WeatherHub> _weatherHub;
    private readonly ILogger<WeatherHostedService> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly WeatherHostedServiceSettings _weatherHostedServiceSettings;
    private readonly IWeatherSubscriberManager _weatherSubscriberManager;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherHostedService"/> class
    /// </summary>
    /// <param name="weatherHub">SignalR hub context</param>
    /// <param name="logger">Logger</param>
    /// <param name="memoryCache">Cache</param>
    /// <param name="appSettings">Настройки приложения</param>
    /// <param name="weatherSubscriberManager">Добавляет, удаляет, получает подписчиков для получения уведомлений
    /// об обновлениях данных погоды для SignalR</param>
    /// <param name="serviceScopeFactory">A factory for creating instances of IServiceScope, which is used to create services within a scope.</param>
    public WeatherHostedService(
        IHubContext<WeatherHub> weatherHub,
        ILogger<WeatherHostedService> logger,
        IMemoryCache memoryCache,
        IOptions<WeatherHostedServiceSettings> appSettings,
        IWeatherSubscriberManager weatherSubscriberManager,
        IServiceScopeFactory serviceScopeFactory
        )
    {
        _weatherHub = weatherHub;
        _logger = logger;
        _memoryCache = memoryCache;
        _weatherHostedServiceSettings = appSettings.Value;
        _weatherSubscriberManager = weatherSubscriberManager;
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    [SuppressMessage(
        "Design",
        "CA1031:Do not catch general exception types",
        Justification = "Не обязательно в данном контексте")]
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (_logger.BeginScope(nameof(WeatherHostedService)))
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await SendWeatherForecastToSubscribersAsync(stoppingToken); 
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, "Ошибка проверки сервиса погоды");
                }
                
                await Task.Delay(_weatherHostedServiceSettings.CheckStatusPause, stoppingToken);
            }
        }
    }

    private async Task SendWeatherForecastToSubscribersAsync(CancellationToken stoppingToken)
    {
        using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
        IWeatherForecastRepository weatherForecastRepository = serviceScope.ServiceProvider.GetRequiredService<IWeatherForecastRepository>();

        IEnumerable<WeatherSubscription> weatherSubscriptions = _weatherSubscriberManager.GetAll();
        foreach (WeatherSubscription weatherSubscription in weatherSubscriptions)
        {
            try
            {
                string cityWeather = await GetCityWeather(weatherSubscription, weatherForecastRepository);
                IClientProxy clientProxy = _weatherHub.Clients.Client(weatherSubscription.ConnectionId);
                await clientProxy.SendAsync(weatherSubscription.ClientMethod, cityWeather, stoppingToken);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Произошла ошибка при обработке подключения: {weatherSubscription.ConnectionId}." +
                                      $"Получение погоды для userId: {weatherSubscription.UserId}.";
                _logger.LogError(errorMessage, ex, nameof(WeatherHostedService));
            }
        }
    }

    private async Task<string> GetCityWeather(WeatherSubscription weatherSubscription, IWeatherForecastRepository weatherForecastRepository)
    {
        string weatherCacheKey = MakeWeatherCaheKey(weatherSubscription);
        if (!_memoryCache.TryGetValue(weatherCacheKey, out string weather))
        {
            return weather;
        }
        
        IEnumerable<WeatherForecast> weatherForecasts = await weatherForecastRepository.GetAsync(weatherSubscription.City, weatherSubscription.Period);
        string jsonWeatherForecasts = JsonHelper.Serialize(weatherForecasts);
        
        SetCache(weatherCacheKey, jsonWeatherForecasts);

        return jsonWeatherForecasts;
    }

    private void SetCache(string weatherCacheKey, string jsonWeatherForecasts)
    {
        MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1024);
        _memoryCache.Set(weatherCacheKey, jsonWeatherForecasts, memoryCacheEntryOptions);
    }

    private string MakeWeatherCaheKey(WeatherSubscription weatherSubscription) =>
        $"Weather_{weatherSubscription.City}_{weatherSubscription.Period.ToString("yyyy-MM-dd")}";
}