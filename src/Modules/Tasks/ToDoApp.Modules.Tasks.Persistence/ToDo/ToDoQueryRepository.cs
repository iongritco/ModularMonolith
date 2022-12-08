﻿using Microsoft.EntityFrameworkCore;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Persistence.ToDo
{
    public class ToDoQueryRepository : IToDoQueryRepository
    {
        private readonly ToDoDataContext _toDoDataContext;

        public ToDoQueryRepository(ToDoDataContext toDoDataContext)
        {
            _toDoDataContext = toDoDataContext;
            //_toDoDataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public async Task<ToDoItem> GetToDo(Guid id, string username)
        {
            return await _toDoDataContext.ToDoItems.Where(x => x.Id.Equals(id) && x.Username.Equals(username)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetToDoList(string username)
        {
            return await _toDoDataContext.ToDoItems.Where(x => x.Username.Equals(username)).ToListAsync();
        }
    }
}
