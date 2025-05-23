using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.EventBus.Events;
using ToDoApp.Modules.Emails.Application.Commands.SendEmail;

namespace ToDoApp.Modules.Emails.API.EventBusConsumers;

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
        _logger.LogInformation("Received a new task completed event for email module: {description}", context.Message.Description);

        await _mediator.Send(new SendEmailCommand(context.Message.Email, context.Message.Description));
    }
}
