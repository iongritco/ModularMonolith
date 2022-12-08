using FluentValidation;

namespace ToDoApp.Modules.Users.Application.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
