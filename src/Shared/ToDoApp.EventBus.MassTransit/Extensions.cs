using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.EventBus.Interfaces;

namespace ToDoApp.EventBus.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.SetInMemorySagaRepositoryProvider();

                var entryAssembly = Assembly.GetEntryAssembly();

                x.AddConsumers(entryAssembly);
                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);

                x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
            });

            services.AddScoped<IEventBus, EventBus>();

            return services;
        }
    }
}
