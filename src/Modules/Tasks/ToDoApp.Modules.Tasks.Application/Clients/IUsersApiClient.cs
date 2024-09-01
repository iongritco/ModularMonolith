using ToDoApp.Modules.Tasks.Application.Clients.DTOs;

namespace ToDoApp.Modules.Tasks.Application.Clients
{
    public interface IUsersApiClient
    {
        Task<UserDto> GetUser(string email);
    }
}
