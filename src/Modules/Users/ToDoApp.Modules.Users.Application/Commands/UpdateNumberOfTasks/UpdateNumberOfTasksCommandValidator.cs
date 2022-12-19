using FluentValidation;

namespace ToDoApp.Modules.Users.Application.Commands.UpdateNumberOfTasks
{
    public class UpdateNumberOfTasksCommandValidator : AbstractValidator<UpdateNumberOfTasksCommand>
    {
        public UpdateNumberOfTasksCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
