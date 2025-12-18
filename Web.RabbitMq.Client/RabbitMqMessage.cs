namespace Web.RabbitMq.Client;

/// <summary>
/// Queue message
/// Сообщение очереди
/// </summary>
/// <typeparam name="T">Тип</typeparam>
public sealed class RabbitMqMessage<T>
{
    public RabbitMqMessage(string command, string correlationId, string type, T data)
    {
        Command = command;
        CorrelationId = correlationId;
        Type = type;
        Data = data;
    }
    
    /// <summary>
    /// Команда
    /// </summary>
    public string Command { get; }

    /// <summary>
    /// Correlation identifier
    /// Идентификатор взаимосвязи
    /// </summary>
    public string CorrelationId { get; }
    
    /// <summary>
    /// Message Type 
    /// Тип сообщения
    /// </summary>
    public string Type { get; }
    
    /// <summary>
    /// Message data
    /// Данные сообщения
    /// </summary>
    public T Data { get; }
}