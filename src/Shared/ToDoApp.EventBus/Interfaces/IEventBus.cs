namespace ToDoApp.EventBus.Interfaces
{
    public interface IEventBus
    {
        Task Publish<T>(T message);
    }
}
