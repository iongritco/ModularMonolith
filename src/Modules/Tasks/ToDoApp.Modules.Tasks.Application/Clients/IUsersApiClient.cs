using ToDoApp.Modules.Tasks.Application.Clients.DTO;

namespace ToDoApp.Modules.Tasks.Application.Clients
{
    public interface IUsersApiClient
    {
        Task<UserDto> GetUser(string email);
    }
}
