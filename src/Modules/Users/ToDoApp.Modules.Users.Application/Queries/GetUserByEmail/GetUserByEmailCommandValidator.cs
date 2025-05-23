using FluentValidation;

namespace ToDoApp.Modules.Users.Application.Queries.GetUserByEmail;

public class GetUserByEmailCommandValidator : AbstractValidator<GetUserByEmailCommand>
{
    public GetUserByEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}
