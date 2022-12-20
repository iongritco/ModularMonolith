using MediatR;
using ToDoApp.Modules.Users.Domain.Entities;

namespace ToDoApp.Modules.Users.Application.Queries.GetUserByEmail
{
    public class GetUserByEmailCommand : IRequest<User>
    {
        public string Email { get; set; }
    }
}
