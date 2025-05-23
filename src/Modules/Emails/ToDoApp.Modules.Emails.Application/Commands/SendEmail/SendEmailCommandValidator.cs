using FluentValidation;

namespace ToDoApp.Modules.Emails.Application.Commands.SendEmail;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
