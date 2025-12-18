namespace Web.RabbitMq.Client.Consumers;

/// <summary>
/// Factory of consumers
/// </summary>
public interface IRabbitMqConsumerFactory : IDisposable
{
    /// <summary>
    /// Returns types of consumers defined in system
    /// Возвращает определённые в системе типы потребителей
    /// </summary>
    /// <returns>Types of consumers</returns>
    IEnumerable<Type> GetConsumers();

    /*/// <summary>
    /// Создает потребителя и обеспечивает доступность экземпляра
    /// Останавливает подписку
    /// </summary>*/
    
    /// <summary>
    /// Creates a consumer and keeps availability of instance
    /// Создает потребителя и обеспечивает доступность экземпляра
    /// </summary>
    /// <param name="consumerType">Type of consumer. Тип потребителя</param>
    /// <returns>Creates a message consumer. Создаёт потребителя сообщений
    /// </returns>
    IRabbitMqConsumer CreateConsumer(Type consumerType);
}