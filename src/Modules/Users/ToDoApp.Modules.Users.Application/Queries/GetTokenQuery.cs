using MediatR;

namespace ToDoApp.Modules.Users.Application.Queries
{
    public class GetTokenQuery : IRequest<string>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
