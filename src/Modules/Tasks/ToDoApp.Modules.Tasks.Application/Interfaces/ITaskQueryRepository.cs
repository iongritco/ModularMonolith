
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Application.Interfaces
{
    public interface IToDoQueryRepository
    {
        Task<IEnumerable<ToDoItem>> GetToDoList(string username);

        Task<ToDoItem> GetToDo(Guid id, string username);
    }
}
