namespace Weather.RabbitMq.Client.Serialization;

/// <summary>
/// Интерфейс для управления способом сериализации сообщений в Json
/// </summary>
public interface IRabbitMqJsonMessageSerializer
{
    /// <summary>
    /// Преобразует объект в JSON
    /// </summary>
    /// <param name="message">Message</param>
    /// <returns>Сериализованный объект</returns>
    string Serialize(object message);
    
    /// <summary>
    /// Преобразует JSON в типизированный объект
    /// </summary>
    /// <param name="message">Message</param>
    /// <typeparam name="T">Type</typeparam>
    /// <returns>Типизированный объект</returns>
    T Deserialize<T>(string message);
}