using MediatR;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Application.Commands.CreateTask
{
    public class CreateToDoCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly IToDoCommandRepository _commandRepository;

        public CreateToDoCommandHandler(IToDoCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var toDo = new ToDoItem(request.Id, request.Description, request.Username);
            await _commandRepository.CreateToDo(toDo);

            return Unit.Value;
        }
    }
}
