using MediatR;
using ToDoApp.Common.Generics;
using ToDoApp.Modules.Users.API.Infrastructure.Interfaces;

namespace ToDoApp.Modules.Users.API.Features.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
	private readonly IIdentityService _identityService;

	public RegisterUserCommandHandler(IIdentityService identityService)
	{
		_identityService = identityService;
	}

	public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		var result = await _identityService.RegisterUser(request.Email, request.Password);

		return result;
	}
}
