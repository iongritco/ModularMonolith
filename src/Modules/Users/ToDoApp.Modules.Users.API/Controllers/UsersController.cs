using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Modules.Users.Application.Commands.RegisterUser;
using ToDoApp.Modules.Users.Application.Queries.GetToken;
using ToDoApp.Modules.Users.Application.Queries.GetUserByEmail;

namespace ToDoApp.Modules.Users.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
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

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand registerUserCommand)
        {
            var result = await _mediator.Send(registerUserCommand);

            if (!result.IsSuccessful)
            {
                Ok(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet]
        [Route("me/name")]
        public IActionResult GetCurrentUser()
        {
            var currentUser = User.Identity.IsAuthenticated ? User.Identity.Name : null;
            return Ok(currentUser);
        }
    }
}
