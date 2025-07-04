using Microsoft.Extensions.Configuration;

namespace ToDoApp.Modules.Users.API.Domain;

public class Settings
{
	private readonly IConfiguration _configuration;

	public Settings(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string TokenKey => _configuration["JwtSecret"];
}
