namespace Web.RabbitMq.Client.Infrastructure;

/// <summary>
/// Предоставляет доступ к CorrelationId в текущем контексте
/// </summary>
public class RabbitMqCorrelationIdContext : IRabbitMqCorrelationIdContext
{
    /// <summary>
    /// Здесь должна быть логика получения CorrelationId
    /// </summary>
    public string CorrelationId => DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
}