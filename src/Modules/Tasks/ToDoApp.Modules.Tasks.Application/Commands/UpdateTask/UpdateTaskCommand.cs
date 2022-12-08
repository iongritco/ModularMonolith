using System.Text.Json.Serialization;
using MediatR;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest
    {
        [JsonConstructor]
        public UpdateTaskCommand()
        {
        }

        public UpdateTaskCommand(string id, string description, int status, string username)
        {
            Id = new Guid(id);
            Description = description;
            Status = (Status)status;
            Username = username;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public string Username { get; set; }
    }
}
