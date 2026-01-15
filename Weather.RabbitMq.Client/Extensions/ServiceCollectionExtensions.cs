using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Weather.RabbitMq.Client.Consumers;
using Weather.RabbitMq.Client.Infrastructure;
using Weather.RabbitMq.Client.Senders;

namespace Weather.RabbitMq.Client.Extensions;

/// <summary>
///     Расширения для RabbitMq
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Регистрирует сервисы RabbitMq
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов</param>
    /// <returns>Изменённая коллекция сервисов</returns>
    public static IServiceCollection AddRabbitMq(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();
        serviceCollection.AddSingleton<IRabbitMqConnectionManager, RabbitMqConnectionManager>();
        serviceCollection.AddSingleton<IRabbitMqConsumerFactory, RabbitMqConsumerFactory>();
        serviceCollection.AddSingleton<IRabbitMqMessageSender, RabbitMqMessageSender>();

        IEnumerable<Type> consumerTypes = RabbitMqConsumerFactory.LoadConsumerTypes();
        foreach (Type consumerType in consumerTypes)
        {
            serviceCollection.AddSingleton(consumerType);
        }

        Type messageHandlerInterface = typeof(IRabbitMqMessageHandler<>);
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        IEnumerable<Type> messageHandlerTypes = assemblies
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(type => type.GetInterfaces()
                               .Any(s => s.IsInterface && s.IsGenericType &&
                                         s.GetGenericTypeDefinition() == messageHandlerInterface)
                           && !type.IsAbstract);
        foreach (Type messageHandlerType in messageHandlerTypes)
        {
            serviceCollection.AddSingleton(messageHandlerType);
        }

        return serviceCollection;
    }
}