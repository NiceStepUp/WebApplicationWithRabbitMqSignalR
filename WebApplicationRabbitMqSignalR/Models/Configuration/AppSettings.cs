using Web.RabbitMq.Client.Configuration;

namespace WebApplicationRabbitMqSignalR.Models.Configuration;

/// <summary>
/// Настройки приложения
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Настройки фонового сервиса для погоды
    /// </summary>
    public WeatherHostedServiceSettings WeatherHostedServiceSettings { get; set; }
    
    /// <summary>
    /// Настройки фонового сервиса для погоды
    /// </summary>
    public RabbitMqConfiguration RabbitMqConfiguration { get; set; }
}