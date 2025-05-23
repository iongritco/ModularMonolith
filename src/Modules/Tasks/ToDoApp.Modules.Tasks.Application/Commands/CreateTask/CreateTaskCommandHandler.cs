using MediatR;
using ToDoApp.Modules.Tasks.Application.Clients;
using ToDoApp.Modules.Tasks.Application.Exceptions;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Application.Commands.CreateTask;

public class CreateToDoCommandHandler : IRequestHandler<CreateTaskCommand>
{
    private readonly ITasksCommandRepository _commandRepository;
    private readonly IUsersApiClient _usersApiClient;

    public CreateToDoCommandHandler(ITasksCommandRepository commandRepository, IUsersApiClient usersApiClient)
    {
        _commandRepository = commandRepository;
        _usersApiClient = usersApiClient;
    }

    public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersApiClient.GetUser(request.Username);
        if (user is null)
        {
            throw new UserNotFoundException(request.Username);
        }

        var toDo = new ToDoItem(request.Id, request.Description, request.Username);
        await _commandRepository.CreateToDo(toDo);
    }
}
