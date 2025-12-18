namespace Web.RabbitMq.Client.Infrastructure;

/// <summary>
/// Предоставляет доступ к CorrelationId в текущем контексте
/// </summary>
public interface IRabbitMqCorrelationIdContext
{
    /// <summary>
    /// Текущий CorrelationId
    /// </summary>
    string CorrelationId { get; }
}