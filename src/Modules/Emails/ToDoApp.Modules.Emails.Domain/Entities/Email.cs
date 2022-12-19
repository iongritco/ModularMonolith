namespace ToDoApp.Modules.Emails.Domain.Entities
{
    public class Email
    {
        public Email(string to, string body)
        {
            To = to;
            Body = body;
        }

        public string To { get; }

        public string Body { get; }
    }
}
