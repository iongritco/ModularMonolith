using MediatR;

namespace ToDoApp.Modules.Emails.Application.Commands.SendEmail
{
    public class SendEmailCommand : IRequest
    {
        public SendEmailCommand(string email, string description)
        {
            Email = email;
            Description = description;
        }

        public string Email { get; }
        public string Description { get; }
    }
}