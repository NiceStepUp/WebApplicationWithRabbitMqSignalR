using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Web.RabbitMq.Client.Extensions;
using Web.RabbitMq.Client.Infrastructure;
using Web.RabbitMq.Client.Serialization;

namespace Web.RabbitMq.Client.Consumers;

/// <summary>
/// Consumer of messages with concrete topology (settings of queue)
/// Потребитель сообщений с конкрентой топологией(настройкой очередей) 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RabbitMqConsumer<T> : IRabbitMqConsumer, IDisposable
{
    private const int DefaultPrefetchSize = 10;
    private readonly TimeSpan _listenerStartedTaskTimeout = TimeSpan.FromSeconds(5);
    private readonly TimeSpan _rabbitMqReconnectionTimeout = TimeSpan.FromMinutes(5);
    private readonly IRabbitMqMessageHandler<T> _rabbitMqMessageHandler;
    private CancellationTokenSource _listenerStartedCancellationTokenSource;
    private Task _listenerStartedTask;
    private readonly ILogger<RabbitMqConsumer<T>> _logger;
    private readonly IJsonMessageSerializer _jsonMessageSerializer;
    private IModel _model;
    private const string RabbitMqOnMyMessageEventName = "My.RabbitMq.OnMessage";

    private static readonly CorrelationIdDiagosticsListener _rabbitMqMessageSource =
        new CorrelationIdDiagosticsListener();

    protected RabbitMqConsumer(
        IRabbitMqConnectionManager connectionManager,
        ILogger<RabbitMqConsumer<T>> logger,
        IRabbitMqMessageHandler<T> rabbitMqMessageHandler,
        IJsonMessageSerializer jsonMessageSerializer)
    {
        RabbitMqConnectionManager = connectionManager;
        _logger = logger;
        _rabbitMqMessageHandler = rabbitMqMessageHandler;
        _jsonMessageSerializer = jsonMessageSerializer ?? new JsonMessageSerializer();
    }
    
    /// <summary>
    /// Менеджер подключений
    /// </summary>
    private IRabbitMqConnectionManager RabbitMqConnectionManager { get; }
    
    /// <summary>
    /// Стартует прослушивание очереди
    /// </summary>
    public void StartConsumer()
    {
        _listenerStartedCancellationTokenSource = new CancellationTokenSource();
        _listenerStartedTask =
            Task.Factory.StartNew(
                StartConsumerConnectionSafe,
                _listenerStartedCancellationTokenSource.Token);
    }

    /// <summary>
    /// Стартует прослушивание очереди до момента подключения
    /// </summary>
    private async Task StartConsumerConnectionSafe()
    {
        int tryCount = 1;
        while (true)
        {
            _listenerStartedCancellationTokenSource.Token.ThrowIfCancellationRequested();

            try
            {
                StartConsumerConnection();
                return;
            }
            catch (BrokerUnreachableException ex)
            {
                _logger.LogCritical(ex, "Cannot establish RabbitMQ connection. TryCount: {tryCount}." +
                                        "Next try in {timeout}", tryCount, _rabbitMqReconnectionTimeout);
                tryCount++;
                
                await Task.Delay(_rabbitMqReconnectionTimeout, _listenerStartedCancellationTokenSource.Token);
            }
        }
    }

    /// <summary>
    /// Стартует прослушивание очереди
    /// </summary>
    private void StartConsumerConnection()
    {
        RabbitMqConnection connection = RabbitMqConnectionManager.GetOrCreateConsumerConnection();
        _model = connection.CreateModel();
        _model.BasicQos(prefetchSize: 0U,prefetchCount: DefaultPrefetchSize, global: false);
        
        string queueName = ConfigureModel(_model);

        AsyncEventingBasicConsumer consumer = new(_model);
        consumer.Received += MessageReceived;
        
        _model.BasicConsume(queueName,  autoAck: false, consumer);
        _logger.LogInformation($"RabbitMQ client consumer started. {GetType().Name}");
    }

    /// <summary>
    /// Обрадотка сообщения
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="args">Аргументы события</param>
    private async Task MessageReceived(object sender, BasicDeliverEventArgs args)
    {
        string command = args.GetHeaderValue(RabbitMqConstants.CommandNameHeader);
        string correlationId = args.GetHeaderValue(RabbitMqConstants.CorrelationIdHeader);
        string requestType = args.GetHeaderValue(RabbitMqConstants.RequestTypeHeader);
        
        using (_logger.BeginScope(command, correlationId))
        {
            if (_rabbitMqMessageSource.IsEnabled(RabbitMqOnMyMessageEventName))
            {
                _rabbitMqMessageSource.Write(correlationId);
            }
            
            string stringBody = Encoding.UTF8.GetString(args.Body.Span);
            string headers = $"command: {command}, correlationId: {correlationId}, requestType: {requestType}";
            _logger.LogInformation("Message received: {stringBody}, headers: {headers} on {type}",
                new object[]{ stringBody, headers, GetType().Name });
            
            T data = _jsonMessageSerializer.Deserialize<T>(stringBody);
            RabbitMqMessage<T> message = new(command,  correlationId, requestType, data);

            try
            {
                await _rabbitMqMessageHandler.Handle(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Message processing error: {message}", e.Message);
            }
            
            _model.BasicAck(deliveryTag: args.DeliveryTag, multiple: true);
            _logger.LogInformation("Message completed: {body}", args: stringBody);
        }
    }

    /// <summary>
    /// Создаёт модель и топологию очередей
    /// </summary>
    /// <param name="model">Модель</param>
    /// <returns>Имя очереди, для которой будет происходить подписка</returns>
    protected abstract string ConfigureModel(IModel model);

    /// <summary>
    /// Останавливает подписку
    /// </summary>
    public void StopConsumer()
    {
        if (_model is { IsOpen: true })
        {
            _model.Close();
        }

        if (_listenerStartedTask == null)
        {
            return;
        }

        try
        {
            if (_listenerStartedTask.IsCompleted)
            {
                return;
            }

            _listenerStartedCancellationTokenSource.Cancel();
            _listenerStartedTask.Wait(_listenerStartedTaskTimeout);
        }
        catch (Exception e)
            when (e is AggregateException or TaskCanceledException or OperationCanceledException)
        {
            _logger.LogError(e, "Consumer stopping exception was thrown");
        }
    }

    
    /// <inheritdoc cref="IDisposable" />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    /// <inheritdoc cref="IDisposable" />
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            StopConsumer();
            
            _model?.Dispose();
        }
    }
}