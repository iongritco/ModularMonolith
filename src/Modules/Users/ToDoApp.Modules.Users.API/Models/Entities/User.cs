namespace ToDoApp.Modules.Users.API.Models.Entities;

public class User
{
	public User(string email, string name, string phoneNumber)
	{
		Email = email;
		Name = name;
		PhoneNumber = phoneNumber;
	}

	public string Email { get; }

	public string Name { get; }

	public string PhoneNumber { get; }
}
