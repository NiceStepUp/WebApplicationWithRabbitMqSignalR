namespace Web.RabbitMq.Client.Consumers;

/// <summary>
/// Interface of message consumer
/// </summary>
public interface IRabbitMqConsumer
{
    /// <summary>
    /// Starts to listen queue
    /// Стартует прослушивание очереди
    /// </summary>
    void StartConsumer();

    /// <summary>
    /// Stops subscription
    /// Останавливает подписку
    /// </summary>
    void StopConsumer();
}