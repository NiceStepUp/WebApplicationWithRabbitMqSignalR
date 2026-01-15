namespace Weather.Common.Models.Configuration;

/// <summary>
/// Настройки фонового сервиса для работы с погодой
/// </summary>
public class WeatherHostedServiceSettings
{
    /// <summary>
    /// Пауза между проверками статуса в миллисекундах
    /// </summary>
    public int CheckStatusPause { get; set; }
}