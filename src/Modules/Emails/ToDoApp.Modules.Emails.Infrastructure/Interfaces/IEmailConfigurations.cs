namespace ToDoApp.Modules.Emails.Infrastructure.Interfaces
{
    public interface IEmailConfigurations
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
