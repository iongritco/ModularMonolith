namespace ToDoApp.SyncBus.Interfaces
{
    public interface ISyncBusSubscriber
    {
        ISyncBusSubscriber Subscribe<TRequest, TResponse>(string key, Func<TRequest, IServiceProvider, Task<TResponse>> action);
    }
}
