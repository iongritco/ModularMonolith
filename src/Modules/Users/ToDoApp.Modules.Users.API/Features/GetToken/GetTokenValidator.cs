using FluentValidation;

namespace ToDoApp.Modules.Users.API.Features.GetToken;

public class GetTokenValidator : AbstractValidator<GetTokenQuery>
{
	public GetTokenValidator()
	{
		RuleFor(x => x.Password).NotEmpty();
		RuleFor(x => x.Username).NotEmpty();
	}
}