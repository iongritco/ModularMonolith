using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Modules.Tasks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("tasks");
        }
    }
}
