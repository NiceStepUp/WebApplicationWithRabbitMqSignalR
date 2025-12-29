namespace WebApplicationRabbitMqSignalR.Models.SignalR;

/// <summary>
/// Запись о подписке на уведомления от мониторинга
/// </summary>
public class WeatherSubscription
{
    /// <summary>
    /// Название целевого метода на фронте
    /// </summary>
    public string ClientMethod { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// SignalR connection Id
    /// </summary>
    public string ConnectionId { get; set; }
    
    /// <summary>
    /// Период
    /// </summary>
    public DateTime Period { get; set; }
    
    /// <summary>
    /// Название города
    /// </summary>
    public string City { get; set; }
}