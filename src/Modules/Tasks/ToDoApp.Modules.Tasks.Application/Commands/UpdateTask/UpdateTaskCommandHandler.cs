using MediatR;
using ToDoApp.EventBus.Events;
using ToDoApp.EventBus.Interfaces;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Commands.UpdateTask
{
    public class UpdateToDoCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITasksCommandRepository _commandRepository;
        private readonly ITasksQueryRepository _queryRepository;
        private readonly IEventBus _eventBus;

        public UpdateToDoCommandHandler(
            ITasksCommandRepository commandRepository,
            ITasksQueryRepository queryRepository,
            IEventBus eventBus)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var toDo = await _queryRepository.GetToDo(request.Id, request.Username);
            toDo.SetDescription(request.Description);
            toDo.SetStatus(request.Status);

            await _commandRepository.UpdateToDo(toDo);

            if (request.Status == Status.Completed)
            {
                await _eventBus.Publish(new TaskCompletedEvent(toDo.Description, toDo.Username));
            }

            return Unit.Value;
        }
    }
}
