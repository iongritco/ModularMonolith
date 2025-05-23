using FluentValidation;

namespace ToDoApp.Modules.Tasks.Application.Commands.CreateTask;

public class CreateToDoValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateToDoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Username).NotNull();
    }
}
