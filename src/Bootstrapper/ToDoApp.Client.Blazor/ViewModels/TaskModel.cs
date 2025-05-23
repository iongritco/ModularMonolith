namespace ToDoApp.Client.Blazor.ViewModels;

public class TaskModel
{
    public TaskModel()
    {
    }

    public TaskModel(Guid id, string? description, string? username, DateTime createdDate, Status status)
    {
        Id = id;
        Description = description;
        Username = username;
        CreatedDate = createdDate;
        Status = status;
    }

    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public Status Status { get; set; }
    public string? Username { get; set; }
}
