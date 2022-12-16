namespace ToDoApp.EventBus.Events
{
    public class TaskCompletedEvent : IntegrationEvent
    {
        public TaskCompletedEvent(string description, string email)
        {
            Description = description;
            Email = email;
        }

        public string Description { get; }

        public string Email { get; }
    }
}
