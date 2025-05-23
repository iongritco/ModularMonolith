using FluentValidation;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Commands.UpdateTask;

public class UpdateToDoValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateToDoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Status).NotEqual(Status.None);
        RuleFor(x => x.Username).NotEmpty();
    }
}
