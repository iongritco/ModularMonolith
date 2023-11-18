using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using ToDoApp.SyncBus.Interfaces;

namespace ToDoApp.SyncBus
{
    public static class Extensions
    {
        public static IServiceCollection AddSyncBus(this IServiceCollection services)
        {
            services.AddSingleton<ISyncBusRegistry, SyncBusRegistry>();
            services.AddSingleton<ISyncBusSubscriber, SyncBusSubscriber>();
            services.AddSingleton<ISyncBusClient, SyncBusClient>();

            return services;
        }

        public static ISyncBusSubscriber UseSyncBus(this IApplicationBuilder app)
            => app.ApplicationServices.GetRequiredService<ISyncBusSubscriber>();
    }
}
