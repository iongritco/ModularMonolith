using FluentValidation;

namespace ToDoApp.Modules.Users.Application.Queries
{
    public class GetTokenValidator : AbstractValidator<GetTokenQuery>
    {
        public GetTokenValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}