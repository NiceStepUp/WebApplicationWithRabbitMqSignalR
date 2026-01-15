namespace Weather.RabbitMq.Client.Senders;

/// <summary>
/// Интерфейс отправителя сообщений в RabbitMq
/// </summary>
public interface IRabbitMqMessageSender
{
    /// <summary>
    /// Отправляет сообщение в RabbitMq
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="exchangeName">Наименование exchange(пункт при)</param>
    /// <param name="commandName">Наименование команды</param>
    /// <param name="requestType">Тип запроса. E.g.: "Graph-Recalculation"</param>
    /// <typeparam name="T">Message type</typeparam>
    void Send<T>(T message, string exchangeName, string commandName, string requestType);


    /// <summary>
    /// Отправляет сообщение в RabbitMq без выбрасывания исключения
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="exchangeName">Наименование exchange(пункт при)</param>
    /// <param name="commandName">Наименование команды</param>
    /// <param name="requestType">Тип запроса. E.g.: "Graph-Recalculation"</param>
    /// <param name="exception">Отдаёт исключение, возникшее в процессе отправки</param>
    /// <typeparam name="T">Тип сообщение</typeparam>
    /// <returns>true - в случае успешной отправки сообщения, иначе - false</returns>
    bool TrySend<T>(T message, string exchangeName, string commandName, string requestType, out Exception exception);
}