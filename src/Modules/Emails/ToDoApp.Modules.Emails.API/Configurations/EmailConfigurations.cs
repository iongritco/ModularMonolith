using ToDoApp.Modules.Emails.Infrastructure.Interfaces;

namespace ToDoApp.Modules.Emails.API.Configurations
{
    public class EmailConfigurations : IEmailConfigurations
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
