using MediatR;

namespace ToDoApp.Modules.Users.API.Features.UpdateNumberOfTasks;

public class UpdateNumberOfTasksCommand : IRequest
{
	public UpdateNumberOfTasksCommand(string email)
	{
		Email = email;
	}

	public string Email { get; }
}