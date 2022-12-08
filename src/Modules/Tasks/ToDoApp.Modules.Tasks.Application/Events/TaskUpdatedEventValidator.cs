using FluentValidation;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Events
{
    public class TaskUpdatedEventValidator : AbstractValidator<TaskUpdatedEvent>
    {
        public TaskUpdatedEventValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Status).NotEqual(Status.None);
        }
    }
}
