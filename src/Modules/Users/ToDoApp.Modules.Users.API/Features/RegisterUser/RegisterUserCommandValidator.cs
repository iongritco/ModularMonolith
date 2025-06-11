using FluentValidation;

namespace ToDoApp.Modules.Users.API.Features.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		RuleFor(x => x.Email).NotEmpty();
		RuleFor(x => x.Password).NotEmpty();
	}
}
