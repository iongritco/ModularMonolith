using FluentValidation;

namespace ToDoApp.Modules.Users.API.Features.UpdateNumberOfTasks;

public class UpdateNumberOfTasksCommandValidator : AbstractValidator<UpdateNumberOfTasksCommand>
{
	public UpdateNumberOfTasksCommandValidator()
	{
		RuleFor(x => x.Email).NotEmpty();
	}
}
