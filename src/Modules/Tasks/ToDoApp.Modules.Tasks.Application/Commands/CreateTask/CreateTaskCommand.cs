using System.Text.Json.Serialization;
using MediatR;

namespace ToDoApp.Modules.Tasks.Application.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest
    {
        [JsonConstructor]
        public CreateTaskCommand()
        { 
        }

        public CreateTaskCommand(string id, string description, string username)
        {
            Id = new Guid(id);
            Description = description;
            Username = username;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Username { get; set; }
    }
}
