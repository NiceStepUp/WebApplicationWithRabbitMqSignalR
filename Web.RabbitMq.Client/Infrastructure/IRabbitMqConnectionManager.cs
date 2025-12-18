namespace Web.RabbitMq.Client.Infrastructure;

/// <summary>
/// Управление пулом подключений
/// Connection pool management
/// </summary>
public interface IRabbitMqConnectionManager
{
    /// <summary>
    /// Отдаёт существующее подключение по имени
    /// </summary>
    /// <param name="connectionName">Имя подключения</param>
    /// <returns>Подключение - если было найдено, в другом случае - NULL</returns>
    RabbitMqConnection GetConnectionByName(string connectionName="default");
    
    /// <summary>
    /// Возвращает новое или существующее подключение для consumer
    /// </summary>
    /// <returns>Подключение к серверу</returns>
    RabbitMqConnection GetOrCreateConsumerConnection();
    
    /// <summary>
    /// Возвращает новое или существующее подключение для sender
    /// </summary>
    /// <returns>Подключение к серверу</returns>
    RabbitMqConnection GetOrCreateSenderConnection();
}