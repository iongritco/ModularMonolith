using Mapster;
using MediatR;
using ToDoApp.Modules.Users.API.Features.GetUserByEmail;
using ToDoApp.Modules.Users.Contracts;
using ToDoApp.Modules.Users.Contracts.DTOs;

namespace ToDoApp.Modules.Users.API.Infrastructure;

public class UsersModuleService : IUsersModuleService
{
	private readonly IMediator _mediator;

	public UsersModuleService(IMediator mediator)
	{
		_mediator = mediator;
	}

	public async Task<UserDto> GetUser(string email)
	{
		var result = await _mediator.Send(new GetUserByEmailCommand(email));

		return result.Adapt<UserDto>();
	}
}
