using RabbitMQ.Client;

namespace Web.RabbitMq.Client.Infrastructure;

/// <summary>
/// Подключение к RabbitMq
/// </summary>
public sealed class RabbitMqConnection : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqConnection"/> class
    /// </summary>
    /// <param name="connection">Соединение с RabbitMq</param>
    public RabbitMqConnection(IConnection connection) =>
        Connection = connection;
    
    /// <summary>
    /// Проверка подключения
    /// </summary>
    public bool IsConnected =>
        Connection.IsOpen;
       
    /// <summary>
    /// Подключение к RabbitMq
    /// </summary>
    public IConnection Connection { get; }
    
    public void Dispose()
    {
        if (Connection.IsOpen)
        {
            Connection.Close();
        }
        
        Connection.Dispose();
    }

    /// <summary>
    /// Создаёт модель подключения
    /// </summary>
    /// <returns>Модель подключения</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("Underlying connection is closed.");
        }
        
        return Connection.CreateModel();
    }
    
    /// <summary>
    /// Закрывает соединение
    /// </summary>
    public void Close() =>
        Connection.Close();
    
    /// <summary>
    /// Закрывает соединение
    /// </summary>
    /// <param name="reasonCode">Код причины закрытия</param>
    /// <param name="reasonText">Описание</param>
    public void Close(ushort reasonCode, string reasonText) =>
        Connection.Close(reasonCode, reasonText);
    
    /// <summary>
    /// Закрывает соединение
    /// </summary>
    /// <param name="timeout">Таймаут закрытия</param>
    public void Close(TimeSpan timeout) =>
        Connection.Close(timeout);
}