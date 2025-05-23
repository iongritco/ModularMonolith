using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Modules.Tasks.Application.Commands.CreateTask;
using ToDoApp.Modules.Tasks.Application.Commands.DeleteTask;
using ToDoApp.Modules.Tasks.Application.Commands.UpdateTask;
using ToDoApp.Modules.Tasks.Application.Queries;

namespace ToDoApp.Modules.Tasks.API.Controllers;

[Route("api/tasks")]
[ApiController]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var result = await _mediator.Send(new GetTasksQuery(User.Identity.Name));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
        command.Username = User.Identity.Name;
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(UpdateTaskCommand command)
    {
        command.Username = User.Identity.Name;
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var command = new DeleteToDoCommand(id, User.Identity.Name);
        await _mediator.Send(command);
        return Ok();
    }
}
