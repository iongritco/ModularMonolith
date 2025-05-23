using FluentValidation;

namespace ToDoApp.Modules.Tasks.Application.Commands.DeleteTask;

public class DeleteToDoValidator :AbstractValidator<DeleteToDoCommand>
{
    public DeleteToDoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Username).NotEmpty();
    }
}
