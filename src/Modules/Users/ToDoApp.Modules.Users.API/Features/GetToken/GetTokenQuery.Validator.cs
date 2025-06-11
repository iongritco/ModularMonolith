using FluentValidation;

namespace ToDoApp.Modules.Users.API.Features.GetToken;

public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
{
	public GetTokenQueryValidator()
	{
		RuleFor(x => x.Password).NotEmpty();
		RuleFor(x => x.Username).NotEmpty();
	}
}