using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Web.RabbitMq.Client.Consumers;

public class RabbitMqConsumerFactory : IRabbitMqConsumerFactory
{
    private readonly Lazy<Type[]> _consumerTypes = new(LoadConsumerTypes, LazyThreadSafetyMode.ExecutionAndPublication);
    private readonly List<IRabbitMqConsumer> _createdConsumers = new();
    private IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider">Service provider</param>
    public RabbitMqConsumerFactory(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;
    
    
    /// <inheritdoc />
    public IEnumerable<Type> GetConsumers() =>
        _consumerTypes.Value;

    public IRabbitMqConsumer CreateConsumer(Type consumerType)
    {
        IRabbitMqConsumer consumer = (IRabbitMqConsumer)_serviceProvider.GetRequiredService(consumerType);
        _createdConsumers.Add(consumer);
        return consumer;
    }

    public void Dispose()
    {
        foreach (IRabbitMqConsumer rabbitMqConsumer in _createdConsumers)
        {
            rabbitMqConsumer.StopConsumer();
            if (rabbitMqConsumer is IDisposable disposableConsumer)
            {
                disposableConsumer.Dispose();
            }
        }
    }
    
    /// <summary>
    /// Получить типы, определяющие потребителей сообщения
    /// </summary>
    /// <returns></returns>
    public static Type[] LoadConsumerTypes()
    {
        Type consumerInterface = typeof(IRabbitMqConsumer);
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        IEnumerable<Type> consumerTypes = assemblies
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(type => consumerInterface.IsAssignableFrom(type) && !type.IsAbstract);
        
        return consumerTypes.ToArray();
    }
}