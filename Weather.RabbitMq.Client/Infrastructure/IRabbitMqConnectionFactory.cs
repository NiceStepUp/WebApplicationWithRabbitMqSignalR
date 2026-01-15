namespace Weather.RabbitMq.Client.Infrastructure;

/// <summary>
/// Connection factory of RabbitMq
/// Фабрика подключений к RabbitMq
/// </summary>
public interface IRabbitMqConnectionFactory
{
    /// <summary>
    /// Creates a new connection
    /// </summary>
    /// <returns>New connection</returns>
    RabbitMqConnection CreateConnection();
    
    /// <summary>
    /// Creates a new connection and adds to ClientProviderName word: " -consumer"
    /// Создает новое подключение и добавляет слово к наименованию ClientProviderName: " -consumer"
    /// </summary>
    /// <returns>New connection with added word "-consumer" to ClientProviderName
    /// Новое подключение с добавленным именем " -consumer" к ClientProviderName</returns>
    RabbitMqConnection CreateConsumerConnection();
    
    /// <summary>
    /// Creates a new connection and adds to ClientProviderName word: " -sender"
    /// Создает новое подключение и добавляет слово к наименованию ClientProviderName: " -sender"
    /// </summary>
    /// <returns>New connection with added word "-sender" to ClientProviderName
    /// Новое подключение с добавленным именем " -sender" к ClientProviderName</returns>
    RabbitMqConnection CreateSenderConnection();
}