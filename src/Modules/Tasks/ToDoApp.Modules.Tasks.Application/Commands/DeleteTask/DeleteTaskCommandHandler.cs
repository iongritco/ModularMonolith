using MediatR;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Commands.DeleteTask
{
    public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand>
    {
        private readonly IToDoCommandRepository _commandRepository;
        private readonly IToDoQueryRepository _queryRepository;

        public DeleteToDoCommandHandler(IToDoCommandRepository commandRepository, IToDoQueryRepository queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task<Unit> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
        {
            var toDo = await _queryRepository.GetToDo(request.Id, request.Username);
            toDo.SetStatus(Status.Deleted);

            await _commandRepository.UpdateToDo(toDo);

            return Unit.Value;
        }
    }
}
