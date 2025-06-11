namespace ToDoApp.Modules.Users.API.Infrastructure.Interfaces;

public interface ITokenService
{
	string GenerateToken(string username);
}
