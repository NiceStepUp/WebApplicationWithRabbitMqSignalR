using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.Common.Helpers;
using Weather.Common.Models.Configuration;
using Weather.Common.Models.Weather;
using Weather.Persistence.Abstractions;
using Weather.RabbitMq.Client.Senders;
using Weather.Services.Abstractions.Interface;

namespace Weather.Services.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IRabbitMqMessageSender _messageSender;
    private readonly ILogger<WeatherForecastService> _logger;
    private readonly IOptions<WeatherConsumerConfiguration> _weatherConsumerConfiguration;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="weatherForecastRepository">Abstraction of weather repository layer</param>
    /// <param name="messageSender">Интерфейс отправителя сообщений в RabbitMq</param>
    /// <param name="logger">Logger</param>
    /// <param name="weatherConsumerConfiguration">Настройки уведомлений графика для RabbitMq</param>
    public WeatherForecastService(
        IWeatherForecastRepository weatherForecastRepository,
        IRabbitMqMessageSender messageSender,
        ILogger<WeatherForecastService> logger,
        IOptions<WeatherConsumerConfiguration> weatherConsumerConfiguration)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _messageSender = messageSender;
        _logger = logger;
        _weatherConsumerConfiguration = weatherConsumerConfiguration;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAsync(string city, DateTime period)
    {
        object message = new { City = city, Period = period }; 
        TrySendMessageToRabbitMq(message, _weatherConsumerConfiguration.Value.ExchangeName, "city-weather-get-started", "weather");
        
        IEnumerable<WeatherForecast> cityWeatherForecasts = await _weatherForecastRepository.GetAsync(city, period);
        
        TrySendMessageToRabbitMq(message, _weatherConsumerConfiguration.Value.ExchangeName, "city-weather-get-completed", "weather");
        
        return cityWeatherForecasts;
    }

    private void TrySendMessageToRabbitMq(object message, string exchangeName, string commandName, string requestType)
    {
        if (_messageSender.TrySend(message, exchangeName, commandName, requestType, out Exception exception))
        {
            _logger.LogError(
                exception,
                $"Ошибка при отправке сообщения в RabbitMq {JsonHelper.Serialize(message)}");
        }
    }
}