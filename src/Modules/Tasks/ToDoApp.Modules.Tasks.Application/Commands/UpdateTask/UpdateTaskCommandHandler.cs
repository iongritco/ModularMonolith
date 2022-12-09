using MediatR;
using ToDoApp.Modules.Tasks.Application.Events;
using ToDoApp.Modules.Tasks.Application.Interfaces;

namespace ToDoApp.Modules.Tasks.Application.Commands.UpdateTask
{
    public class UpdateToDoCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITasksCommandRepository _commandRepository;
        private readonly ITasksQueryRepository _queryRepository;
        private readonly IMediator _mediator;

        public UpdateToDoCommandHandler(
            ITasksCommandRepository commandRepository,
            ITasksQueryRepository queryRepository,
            IMediator mediator)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var toDo = await _queryRepository.GetToDo(request.Id, request.Username);
            toDo.SetDescription(request.Description);
            toDo.SetStatus(request.Status);

            await _commandRepository.UpdateToDo(toDo);
            await _mediator.Publish(new TaskUpdatedEvent(toDo.Username, toDo.Description, toDo.Status), cancellationToken);

            return Unit.Value;
        }
    }
}
