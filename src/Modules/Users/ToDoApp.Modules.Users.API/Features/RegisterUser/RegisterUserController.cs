using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Modules.Users.API.Features.RegisterUser;

[Route("api/users")]
[ApiController]
public class RegisterUserController : ControllerBase
{
	private readonly IMediator _mediator;

	public RegisterUserController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[AllowAnonymous]
	[Route("register")]
	public async Task<IActionResult> RegisterUser(RegisterUserCommand registerUserCommand)
	{
		var result = await _mediator.Send(registerUserCommand);

		if (!result.IsSuccessful)
		{
			return BadRequest(result.ErrorMessage);
		}

		return Ok();
	}
}
