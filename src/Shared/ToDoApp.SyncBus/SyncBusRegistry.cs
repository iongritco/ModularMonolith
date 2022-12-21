using ToDoApp.SyncBus.Entities;
using ToDoApp.SyncBus.Interfaces;

namespace ToDoApp.SyncBus
{
    public class SyncBusRegistry : ISyncBusRegistry
    {
        private readonly Dictionary<string, SyncBusRegistration> _syncBusRegistrations = new();

        public SyncBusRegistration GetSyncBusRegistration(string key) =>
            _syncBusRegistrations.TryGetValue(key, out var registration) ? registration : null;

        public void AddSyncBusAction(string key, Type requestType, Type responseType, Func<object, Task<object>> action)
        {
            if (key is null)
            {
                throw new InvalidOperationException("Action key cannot be null");
            }

            var registration = new SyncBusRegistration(requestType, responseType, action);
            _syncBusRegistrations.Add(key, registration);
        }
    }
}
