namespace Weather.RabbitMq.Client;

/// <summary>
/// Constants
/// </summary>
public static class RabbitMqConstants
{
    /// <summary>
    /// Имя заголовка для типа запроса
    /// Header name for header type
    /// </summary>
    public const string RequestTypeHeader = "RequestType";
    
    /// <summary>
    /// Имя заголовка для имени команды
    /// Header name for command name
    /// </summary>
    public const string CommandNameHeader = "CommandName";
    
    /// <summary>
    /// Имя заголовка для идентификатора взаимосвязи
    /// Header name for correlation identifier
    /// </summary>
    public const string CorrelationIdHeader = "CorrelationId";
}