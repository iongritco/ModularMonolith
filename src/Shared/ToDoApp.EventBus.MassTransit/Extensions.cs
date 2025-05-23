using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.EventBus.Interfaces;

namespace ToDoApp.EventBus.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.SetInMemorySagaRepositoryProvider();

            x.AddConsumers(assemblies);
            x.AddSagaStateMachines(assemblies);
            x.AddSagas(assemblies);
            x.AddActivities(assemblies);

            x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });

        services.AddScoped<IEventBus, EventBus>();

        return services;
    }
}
