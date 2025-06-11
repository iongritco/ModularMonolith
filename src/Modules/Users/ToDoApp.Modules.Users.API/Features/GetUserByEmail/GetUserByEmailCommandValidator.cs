using FluentValidation;

namespace ToDoApp.Modules.Users.API.Features.GetUserByEmail;

public class GetUserByEmailCommandValidator : AbstractValidator<GetUserByEmailCommand>
{
	public GetUserByEmailCommandValidator()
	{
		RuleFor(x => x.Email).NotEmpty();
	}
}
