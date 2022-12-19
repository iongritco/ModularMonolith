using MediatR;
using ToDoApp.Modules.Emails.Application.Interfaces;

namespace ToDoApp.Modules.Emails.Application.Commands.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
    {
        private readonly IEmailService _emailService;

        public SendEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmail(new Domain.Entities.Email(request.Email, request.Description));
            return Unit.Value;
        }
    }
}
