using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Modules.Users.API.Features.GetToken;

[Route("api/users")]
[ApiController]
public class GetTokenController : ControllerBase
{
	private readonly IMediator _mediator;

	public GetTokenController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[AllowAnonymous]
	[Route("login")]
	public async Task<IActionResult> Login(GetTokenQuery getTokenQuery)
	{
		var result = await _mediator.Send(getTokenQuery);
		if (string.IsNullOrEmpty(result))
		{
			return Forbid();
		}

		return Ok(result);
	}
}
