using Microsoft.Extensions.Logging;
using ToDoApp.Modules.Emails.Application.Interfaces;
using ToDoApp.Modules.Emails.Domain.Entities;
using ToDoApp.Modules.Emails.Infrastructure.Interfaces;

namespace ToDoApp.Modules.Emails.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IEmailConfigurations _configurations;

        public EmailService(ILogger<EmailService> logger, IEmailConfigurations configurations)
        {
            _logger = logger;
            _configurations = configurations;
        }

        public Task SendEmail(Email email)
        {
            _logger.LogInformation("Sending email to {to} with body {body} using smtp {smtp} and port {port}", email.To,
                email.Body, _configurations.SmtpServer, _configurations.Port);

            return Task.CompletedTask;
        }
    }
}
