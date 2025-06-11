using ToDoApp.Modules.Users.Contracts.DTOs;

namespace ToDoApp.Modules.Users.Contracts;

public interface IUsersModuleService
{
	public Task<UserDto> GetUser(string email);
}
