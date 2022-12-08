using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Modules.Users.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("users");
        }
    }
}
