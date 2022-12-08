using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Persistence.ToDo
{
    public class ToDoCommandRepository : IToDoCommandRepository
    {
        private readonly ToDoDataContext _toDoDataContext;

        public ToDoCommandRepository(ToDoDataContext toDoDataContext)
        {
            _toDoDataContext = toDoDataContext;
        }

        public async Task CreateToDo(ToDoItem toDo)
        {
            _toDoDataContext.Add(toDo);
            await _toDoDataContext.SaveChangesAsync();
        }

        public async Task UpdateToDo(ToDoItem toDo)
        {
            _toDoDataContext.Update(toDo);
            await _toDoDataContext.SaveChangesAsync();
        }
    }
}
