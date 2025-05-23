using ToDoApp.Common.Interfaces;
using ToDoApp.SyncBus.Interfaces;

namespace ToDoApp.SyncBus;

public class SyncBusClient : ISyncBusClient
{
    private readonly ISyncBusRegistry _syncBusRegistry;
    private readonly ISerializer _serializer;

    public SyncBusClient(ISyncBusRegistry syncBusRegistry, ISerializer serializer)
    {
        _syncBusRegistry = syncBusRegistry;
        _serializer = serializer;
    }

    public async Task<TResult> SendAsync<TResult>(string key, object request) where TResult : class
    {
        var registration = _syncBusRegistry.GetSyncBusRegistration(key);
        if (registration is null)
        {
            throw new InvalidOperationException($"No action has been defined for key: '{key}'.");
        }

        var receiverRequest = _serializer.TranslateType(request, registration.RequestType);
        var result = await registration.Action(receiverRequest);

        return result is null ? null : _serializer.TranslateType<TResult>(result);
    }
}
