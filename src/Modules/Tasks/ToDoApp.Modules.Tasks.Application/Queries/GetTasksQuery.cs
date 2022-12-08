using MediatR;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Application.Queries
{
    public class GetTasksQuery : IRequest<List<ToDoItem>>
    {
        public GetTasksQuery(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }
    }
}
