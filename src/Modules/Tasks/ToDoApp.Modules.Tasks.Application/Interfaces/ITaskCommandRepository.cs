
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Application.Interfaces;

public interface ITasksCommandRepository
{
    Task CreateToDo(ToDoItem toDo);

    Task UpdateToDo(ToDoItem toDo);
}
