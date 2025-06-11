using ToDoApp.Common.Generics;
using ToDoApp.Modules.Users.API.Models.Entities;

namespace ToDoApp.Modules.Users.API.Infrastructure.Interfaces;

public interface IIdentityService
{
	Task<bool> Authenticate(string username, string password);

	Task<Result> RegisterUser(string email, string password);

	Task UpdateNumberOfTasks(string email);

	Task<Result<User>> GetUserByEmail(string email);
}
