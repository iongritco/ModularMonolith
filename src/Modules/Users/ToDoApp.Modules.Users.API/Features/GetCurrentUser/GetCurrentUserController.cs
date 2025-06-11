using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Modules.Users.API.Features.GetCurrentUser;

[Route("api/users")]
[ApiController]
[Authorize]
public class GetCurrentUserController : ControllerBase
{
	[HttpGet]
	[Route("me/name")]
	public IActionResult GetCurrentUser()
	{
		var currentUser = User.Identity.IsAuthenticated ? User.Identity.Name : null;
		return Ok(currentUser);
	}
}
