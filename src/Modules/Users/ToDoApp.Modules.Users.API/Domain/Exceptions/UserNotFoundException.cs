namespace ToDoApp.Modules.Users.API.Domain.Exceptions;

public class UserNotFoundException : Exception
{
	public UserNotFoundException(string email)
		: base($"User with email {email} was not found.")
	{
	}
}