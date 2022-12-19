using Microsoft.Extensions.Logging;
using ToDoApp.Modules.Emails.Application.Interfaces;
using ToDoApp.Modules.Emails.Domain.Entities;

namespace ToDoApp.Modules.Emails.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmail(Email email)
        {
            _logger.LogInformation("Sending email to {to} with body {body}", email.To, email.Body);

            return Task.CompletedTask;
        }
    }
}
