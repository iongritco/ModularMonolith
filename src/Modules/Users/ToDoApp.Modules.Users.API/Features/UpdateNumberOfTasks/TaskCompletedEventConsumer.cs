using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.EventBus.Events;

namespace ToDoApp.Modules.Users.API.Features.UpdateNumberOfTasks;

public class TaskCompletedEventConsumer : IConsumer<TaskCompletedEvent>
{
	private readonly ILogger<TaskCompletedEventConsumer> _logger;
	private readonly IMediator _mediator;

	public TaskCompletedEventConsumer(ILogger<TaskCompletedEventConsumer> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	public async Task Consume(ConsumeContext<TaskCompletedEvent> context)
	{
		_logger.LogInformation("Received a new task completed event for users module: {description}", context.Message.Description);

		await _mediator.Send(new UpdateNumberOfTasksCommand(context.Message.Email));
	}
}
