using ToDoApp.Modules.Tasks.Application.Clients;
using ToDoApp.Modules.Tasks.Application.Clients.DTO;

namespace ToDoApp.Modules.Tasks.Infrastructure
{
    public class UsersApiClient : IUsersApiClient
    {
        public Task<UserDto> GetUser(string email)
        {
            return Task.FromResult(new UserDto());
        }
    }
}
