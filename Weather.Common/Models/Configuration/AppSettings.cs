namespace Weather.Common.Models.Configuration;

/// <summary>
/// Настройки приложения
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Настройки фонового сервиса для погоды
    /// </summary>
    public WeatherHostedServiceSettings WeatherHostedServiceSettings { get; set; }
}