using FluentValidation;

namespace ToDoApp.Modules.Tasks.Application.Queries
{
    public class GetToDoListQueryValidator : AbstractValidator<GetTasksQuery>
    {
        public GetToDoListQueryValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}

