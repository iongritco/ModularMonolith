using Microsoft.EntityFrameworkCore;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Persistence
{
    public class ToDoDataContext : DbContext
    {
        public ToDoDataContext(DbContextOptions<ToDoDataContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
