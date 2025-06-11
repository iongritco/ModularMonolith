using MediatR;

namespace ToDoApp.Modules.Users.API.Features.GetToken;

public class GetTokenQuery : IRequest<string>
{
	public string Username { get; set; }

	public string Password { get; set; }
}
