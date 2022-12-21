namespace ToDoApp.SyncBus.Entities
{
    public class SyncBusRegistration
    {
        public SyncBusRegistration(Type requestType, Type responseType, Func<object, Task<object>> action)
        {
            RequestType = requestType;
            ResponseType = responseType;
            Action = action;
        }

        public Type RequestType { get; }

        public Type ResponseType { get; }

        public Func<object, Task<object>> Action { get; }
    }
}