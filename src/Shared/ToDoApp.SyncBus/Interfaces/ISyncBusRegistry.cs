using ToDoApp.SyncBus.Entities;

namespace ToDoApp.SyncBus.Interfaces;

public interface ISyncBusRegistry
{
    SyncBusRegistration GetSyncBusRegistration(string key);

    void AddSyncBusAction(string key, Type requestType, Type responseType, Func<object, Task<object>> action);
}
