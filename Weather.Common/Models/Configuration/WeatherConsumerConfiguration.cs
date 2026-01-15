namespace Weather.Common.Models.Configuration;

/// <summary>
/// Настройки уведомлений графика для RabbitMq
/// </summary>
public class WeatherConsumerConfiguration
{
    /// <summary>
    /// Название точки обмена RabbitMq
    /// </summary>
    public string ExchangeName { get; set; }
    
    /// <summary>
    /// Префетчинг сообщений
    /// </summary>
    public string PrefetchCount { get; set; }
}