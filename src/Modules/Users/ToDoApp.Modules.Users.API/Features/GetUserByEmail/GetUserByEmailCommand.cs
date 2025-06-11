using MediatR;
using ToDoApp.Modules.Users.API.Models.Entities;

namespace ToDoApp.Modules.Users.API.Features.GetUserByEmail;

public class GetUserByEmailCommand : IRequest<User>
{
	public GetUserByEmailCommand(string email)
	{
		Email = email;
	}

	public string Email { get; }
}
