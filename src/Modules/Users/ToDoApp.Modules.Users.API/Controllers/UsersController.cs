using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Modules.Users.API.Controllers
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
