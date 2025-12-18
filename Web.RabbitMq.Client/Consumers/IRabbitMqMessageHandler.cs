namespace Web.RabbitMq.Client.Consumers;

/// <summary>
/// Interface of message handler
/// Интерфейс обработчика сообщений
/// </summary>
public interface IRabbitMqMessageHandler<T>
{
    /// <summary>
    /// Handle a message
    /// </summary>
    /// <param name="message">Message</param>
    /// <returns>Task</returns>
    Task Handle(RabbitMqMessage<T> message);
}