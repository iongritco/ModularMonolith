namespace ToDoApp.Client.Blazor.ViewModels
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Status Status { get; set; }
        public string Username { get; set; }
    }
}
