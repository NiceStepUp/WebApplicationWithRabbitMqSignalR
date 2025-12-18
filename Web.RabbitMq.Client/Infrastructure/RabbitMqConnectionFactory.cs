using System.Security.Authentication;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Web.RabbitMq.Client.Configuration;

namespace Web.RabbitMq.Client.Infrastructure;

/// <summary>
/// Фабрика подключений к RabbitMq
/// </summary>
public class RabbitMqConnectionFactory : IRabbitMqConnectionFactory
{
    private readonly ConnectionFactory _connectionFactory;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="rabbitMqConfiguration">Настройки подключения</param>
    public RabbitMqConnectionFactory(IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        _connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConfiguration.Value.HostName,
            UserName = rabbitMqConfiguration.Value.UserName,
            Password = rabbitMqConfiguration.Value.Password,
            Port = rabbitMqConfiguration.Value.Port,
            VirtualHost = rabbitMqConfiguration.Value.VirtualHost,
            ClientProvidedName = rabbitMqConfiguration.Value.ApplicationName,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            DispatchConsumersAsync = true
        };

        if (rabbitMqConfiguration.Value.CanEnableSsl)
        {
            _connectionFactory.Ssl.Enabled = true;
            _connectionFactory.AmqpUriSslProtocols = SslProtocols.Tls12;

            if (rabbitMqConfiguration.Value.CanIgnoreCertificationValidation)
            {
                _connectionFactory.Ssl.CertificateValidationCallback = 
                    (sender, certificate, chain, sslPolicyErrors) => true;
            }
        }
    }

    /// <summary>
    /// Создаёт новое подключение
    /// </summary>
    /// <returns>Новое подключение</returns>
    public RabbitMqConnection CreateConnection()
    {
        IConnection connection = _connectionFactory.CreateConnection();
        return new RabbitMqConnection(connection);
    }

    /// <summary>
    /// Создаёт новое подключение и добавляет к имени слово " - consumer"
    /// </summary>
    /// <returns>Новое подключение с добавленным именем " - consumer"</returns>
    public RabbitMqConnection CreateConsumerConnection() =>
        CreateNamedConnection("consumer");
    
    /// <summary>
    /// Создаёт новое подключение и добавляет к имени слово " - sender"
    /// </summary>
    /// <returns>Новое подключение с добавленным именем " - sender"</returns>
    public RabbitMqConnection CreateSenderConnection() =>
        CreateNamedConnection("sender");
    
    /// <summary>
    /// Создаёт новое подключение
    /// </summary>
    /// <returns>Новое подключение</returns>
    private RabbitMqConnection CreateNamedConnection(string name)
    {
        IConnection connection = _connectionFactory.CreateConnection(_connectionFactory.ClientProvidedName + " - " + name);
        return new RabbitMqConnection(connection);
    }
}