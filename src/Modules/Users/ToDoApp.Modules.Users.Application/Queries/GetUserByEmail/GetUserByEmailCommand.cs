using MediatR;
using ToDoApp.Modules.Users.Domain.Entities;

namespace ToDoApp.Modules.Users.Application.Queries.GetUserByEmail
{
    public class GetUserByEmailCommand : IRequest<User>
    {
        public GetUserByEmailCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
