using MediatR;

namespace ToDoApp.Modules.Users.Application.Commands.UpdateNumberOfTasks;

public class UpdateNumberOfTasksCommand : IRequest
{
    public UpdateNumberOfTasksCommand(string email)
    {
        Email = email;
    }

    public string Email { get; }
}