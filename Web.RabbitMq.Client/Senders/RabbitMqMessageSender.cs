using System.Text;
using RabbitMQ.Client;
using Web.RabbitMq.Client.Infrastructure;
using Web.RabbitMq.Client.Serialization;

namespace Web.RabbitMq.Client.Senders;

/// <inheritdoc cref="IRabbitMqMessageSender" />
public class RabbitMqMessageSender : IRabbitMqMessageSender
{
    private readonly IRabbitMqConnectionManager _connectionManager;
    private readonly IRabbitMqCorrelationIdContext _correlationIdContext;
    private readonly IJsonMessageSerializer _jsonMessageSerializer;

    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="correlationIdContext">Предоставляет доступ к CorrelationId в текущем контексте</param>
    /// <param name="connectionManager">Класс для управления пулом подключений</param>
    /// <param name="jsonMessageSerializer">Сериализатор сообщений</param>
    public RabbitMqMessageSender(
        IRabbitMqCorrelationIdContext correlationIdContext,
        IRabbitMqConnectionManager connectionManager,
        IJsonMessageSerializer jsonMessageSerializer)
    {
        _correlationIdContext = correlationIdContext;
        _connectionManager = connectionManager;
        _jsonMessageSerializer = jsonMessageSerializer;
    }

    /// <inheritdoc
    public void Send<T>(T message, string exchangeName, string commandName, string requestType)
    {
        string jsonMessage = _jsonMessageSerializer.Serialize(message);
        ValidateSendParameters(jsonMessage, exchangeName, commandName, requestType);

        RabbitMqConnection connection = _connectionManager.GetOrCreateSenderConnection();
        using IModel channel = connection.CreateModel();
        IBasicProperties basicProperties = MakeBasicProperties(commandName, channel, requestType);

        channel.BasicPublish(
            exchangeName,
            string.Empty,
            basicProperties,
            Encoding.UTF8.GetBytes(jsonMessage));
    }

    public bool TrySend<T>(T message, string exchangeName, string commandName, string requestType,
        out Exception exception)
    {
        exception = null;
        try
        {
            Send(message, exchangeName, commandName, requestType);
            
            return true;
        }
        catch (Exception e)
        {
            exception = e;
            return false;
        }
    }

    private IBasicProperties MakeBasicProperties(string commandName, IModel channel, string requestType)
    {
        IBasicProperties basicProperties = channel.CreateBasicProperties();
        basicProperties.ContentType = "text/plain";
        basicProperties.DeliveryMode = 2;
        basicProperties.Expiration = "36000000";

        Dictionary<string, object> headers = new()
        {
            { "RequestType", requestType },
            { "CommandName", commandName },
            { "CorrelationId", _correlationIdContext.CorrelationId }
        };
        basicProperties.Headers = headers;

        return basicProperties;
    }

    private void ValidateSendParameters(string message, string exchangeName, string commandName, string requestType)
    {
        ThrowArgumentNullExceptionIfNullOrEmpty(message, nameof(message));
        
        ThrowArgumentNullExceptionIfNullOrEmpty(exchangeName, nameof(exchangeName));
        
        ThrowArgumentNullExceptionIfNullOrEmpty(commandName, nameof(commandName));
        
        ThrowArgumentNullExceptionIfNullOrEmpty(requestType, nameof(requestType));
    }

    private void ThrowArgumentNullExceptionIfNullOrEmpty(string text, string name)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException($"{name} is null or empty");
        }
    }
}