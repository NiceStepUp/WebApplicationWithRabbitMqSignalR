namespace WebApplicationRabbitMqSignalR.Models.SignalR.Requests;

/// <summary>
///     Модель запроса на подписку с фронта
/// </summary>
public class WeatherSubscriptionRequest
{
    /// <summary>
    ///     Название целевого метода на фронте
    /// </summary>
    public string ClientMethod { get; set; }

    /// <summary>
    ///     Период
    /// </summary>
    public DateTime Period { get; set; }

    /// <summary>
    ///     Город
    /// </summary>
    public string City { get; set; }
}