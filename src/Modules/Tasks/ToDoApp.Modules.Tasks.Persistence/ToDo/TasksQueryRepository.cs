using Microsoft.EntityFrameworkCore;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Persistence.ToDo
{
    public class TasksQueryRepository : ITasksQueryRepository
    {
        private readonly TasksContext _tasksContext;

        public TasksQueryRepository(TasksContext tasksContext)
        {
            _tasksContext = tasksContext;
            //_tasksContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public async Task<ToDoItem> GetToDo(Guid id, string username)
        {
            return await _tasksContext.ToDoItems.Where(x => x.Id.Equals(id) && x.Username.Equals(username)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetToDoList(string username)
        {
            return await _tasksContext.ToDoItems.Where(x => x.Username.Equals(username)).ToListAsync();
        }
    }
}
