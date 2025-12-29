using Microsoft.AspNetCore.SignalR;
using WebApplicationRabbitMqSignalR.Models.SignalR;
using WebApplicationRabbitMqSignalR.Models.SignalR.Requests;
using WebApplicationRabbitMqSignalR.Services.SignalR.Interfaces;

namespace WebApplicationRabbitMqSignalR.SignalR;

/// <summary>
/// SignalR хаб для обмена сообщениями с web клиентом
/// </summary>
public class WeatherHub : Hub
{
    private readonly IWeatherSubscriberManager _weatherSubscriberManager;
    private readonly ILogger<WeatherHub> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherHub"/> class
    /// </summary>
    /// <param name="weatherSubscriberManager">Добавляет, удаляет, получает подписчиков для получения уведомлений
    /// об обновлениях данных погоды для SignalR</param>
    /// <param name="logger">Logger</param>
    public WeatherHub(IWeatherSubscriberManager weatherSubscriberManager, ILogger<WeatherHub> logger)
    {
        _weatherSubscriberManager = weatherSubscriberManager;
        _logger = logger;
    }

    /// inheritdoc/>
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (!_weatherSubscriberManager.Remove(Context.ConnectionId))
        {
            _logger.LogWarning($"Не удалось удалить информацию о подключении" +
                               $"ConnectionId: { Context.ConnectionId }." +
                               $"UserIdentifier: {Context.UserIdentifier}");
        }
        
        await base.OnDisconnectedAsync(exception);
    }
    
    /// <summary>
    /// Подписывает(добавялет подписчика) с фронта(Angular, ReactJs) на уведмоление от погоды с сервера(C#)
    /// </summary>
    /// <param name="weatherSubscriptionRequest"></param>
    public void SubscribeToWeather(WeatherSubscriptionRequest weatherSubscriptionRequest)
    {
        ArgumentNullException.ThrowIfNull(weatherSubscriptionRequest);

        WeatherSubscription weatherSubscription = new()
        {
            ConnectionId = Context.ConnectionId,
            ClientMethod = weatherSubscriptionRequest.ClientMethod,
            UserId = Context.UserIdentifier,
            Period = weatherSubscriptionRequest.Period,
            City = weatherSubscriptionRequest.City
        };
        _weatherSubscriberManager.AddOrUpdate(weatherSubscription);
    }
}