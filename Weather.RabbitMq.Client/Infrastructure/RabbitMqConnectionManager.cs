using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Weather.RabbitMq.Client.Infrastructure;

/// <summary>
///     Класс для управления пулом подключений
/// </summary>
public sealed class RabbitMqConnectionManager : IRabbitMqConnectionManager, IDisposable
{
    private readonly IRabbitMqConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMqConnectionManager> _logger;

    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="connectionFactory">Фабрика подключений</param>
    /// <param name="logger">Логгер</param>
    public RabbitMqConnectionManager(IRabbitMqConnectionFactory connectionFactory,
        ILogger<RabbitMqConnectionManager> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    private ConcurrentDictionary<string, RabbitMqConnection> Connections => new();

    public void Dispose()
    {
        foreach (KeyValuePair<string, RabbitMqConnection> rabbitMqConnection in Connections)
        {
            UnsubscribeConnectionEvents(rabbitMqConnection.Value.Connection);
            rabbitMqConnection.Value.Dispose();
        }
    }

    public RabbitMqConnection GetConnectionByName(string connectionName = "default") =>
        Connections.GetValueOrDefault(connectionName);

    /// <summary>
    ///     Возвращает новое или существуюшее подключение для consumer
    /// </summary>
    /// <returns>Подключение к серверу</returns>
    public RabbitMqConnection GetOrCreateConsumerConnection() =>
        GetOrCreateConnection(
            "consumer-connection",
            () => _connectionFactory.CreateConsumerConnection());

    /// <summary>
    ///     Возвращает новое или существуюшее подключение для sender
    /// </summary>
    /// <returns>Подключение к серверу</returns>
    public RabbitMqConnection GetOrCreateSenderConnection() =>
        GetOrCreateConnection(
            "sender-connection",
            () => _connectionFactory.CreateSenderConnection());

    /// <summary>
    ///  Возвращает новое или существующее подключение по имени
    /// </summary>
    /// <param name="connectionName">Имя подключения</param>
    /// <param name="createConnectionFactory">Создание соединения RabbitMq</param>
    /// <returns>Подключение к серверу</returns>
    public RabbitMqConnection GetOrCreateConnection(string connectionName,
        Func<RabbitMqConnection> createConnectionFactory)
    {
        try
        {
            return Connections.GetOrAdd(connectionName, name =>
            {
                RabbitMqConnection rabbitMqConnection = createConnectionFactory();
                SubscribeConnectionEvents(rabbitMqConnection.Connection);
                return rabbitMqConnection;
            });
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Failed to get or create connection '{connectionName}'", ex);
        }
    }

    private void SubscribeConnectionEvents(IConnection connection)
    {
        connection.ConnectionShutdown += OnConnectionShutdown;
        connection.CallbackException += OnCallbackException;
        connection.ConnectionBlocked += OnConnectionBlocked;
        connection.ConnectionUnblocked += OnConnectionUnblocked;
    }

    private void UnsubscribeConnectionEvents(IConnection connection)
    {
        connection.ConnectionShutdown -= OnConnectionShutdown;
        connection.CallbackException -= OnCallbackException;
        connection.ConnectionBlocked -= OnConnectionBlocked;
        connection.ConnectionUnblocked -= OnConnectionUnblocked;
    }

    private void OnConnectionUnblocked(object sender, EventArgs e) =>
        _logger.LogInformation($"Connection [{GetProviderName(sender)} connection] Unblocked.");

    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e) =>
        _logger.LogInformation($"Connection [{GetProviderName(sender)} connection] Blocked.");

    private void OnCallbackException(object sender, CallbackExceptionEventArgs e) =>
        _logger.LogError(
            e.Exception,
            $"Connection [{GetProviderName(sender)}  connection] CallbackException."
            + $" Message [{e.Exception.Message}{Environment.NewLine}" +
            $"StackTrace: {e.Exception.StackTrace}");

    private void OnConnectionShutdown(object sender, ShutdownEventArgs e) =>
        _logger.LogInformation($"Connection [{GetProviderName(sender)} connection] Shutdown." +
                               $" Cause: {e.Cause}. Reply test {e.ReplyText}");

    private string GetProviderName(object connection)
    {
        if (connection is IConnection rabbitConnection)
        {
            return rabbitConnection.ClientProvidedName;
        }

        return string.Empty;
    }
}