using MediatR;

namespace ToDoApp.Modules.Tasks.Application.Commands.DeleteTask
{
    public class DeleteToDoCommand : IRequest
    {
        public DeleteToDoCommand(Guid id, string username)
        {
            Id = id;
            Username = username;
        }

        public Guid Id { get; }

        public string Username { get; }
    }
}
