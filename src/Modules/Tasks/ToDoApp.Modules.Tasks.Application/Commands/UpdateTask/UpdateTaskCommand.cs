using MediatR;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public string Username { get; set; }
    }
}
