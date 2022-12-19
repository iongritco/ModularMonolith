using ToDoApp.Modules.Emails.Domain.Entities;

namespace ToDoApp.Modules.Emails.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(Email email);
    }
}
