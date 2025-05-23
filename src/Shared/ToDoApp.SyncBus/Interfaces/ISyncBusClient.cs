namespace ToDoApp.SyncBus.Interfaces;

public interface ISyncBusClient
{
    Task<TResult> SendAsync<TResult>(string key, object request) where TResult : class;
}
