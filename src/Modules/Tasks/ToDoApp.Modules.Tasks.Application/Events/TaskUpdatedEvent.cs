using MediatR;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Events
{
    public class TaskUpdatedEvent : INotification
    {
        public string Email { get; }

        public string Description { get; }

        public Status Status { get; }

        public TaskUpdatedEvent(string email, string description, Status status)
        {
            Email = email;
            Description = description;
            Status = status;
        }
    }
}
