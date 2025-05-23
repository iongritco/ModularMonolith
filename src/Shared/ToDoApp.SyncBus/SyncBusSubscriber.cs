using Microsoft.Extensions.DependencyInjection;
using ToDoApp.SyncBus.Interfaces;

namespace ToDoApp.SyncBus;

internal class SyncBusSubscriber : ISyncBusSubscriber
{
    private readonly ISyncBusRegistry _syncBusRegistry;
    private readonly IServiceProvider _serviceProvider;

    public SyncBusSubscriber(ISyncBusRegistry syncBusRegistry, IServiceProvider serviceProvider)
    {
        _syncBusRegistry = syncBusRegistry;
        _serviceProvider = serviceProvider;
    }

    public ISyncBusSubscriber Subscribe<TRequest, TResponse>(string key,
        Func<TRequest, IServiceProvider, Task<TResponse>> action)
    {
        _syncBusRegistry.AddSyncBusAction(key, typeof(TRequest), typeof(TResponse),
            async request =>
            {
                using var scope = _serviceProvider.CreateScope();
                return await action((TRequest)request, scope.ServiceProvider);
            });

        return this;
    }
}
