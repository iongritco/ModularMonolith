using Microsoft.EntityFrameworkCore;
using ToDoApp.Modules.Tasks.Domain.Entities;

namespace ToDoApp.Modules.Tasks.Persistence
{
    public class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tasks");
            base.OnModelCreating(modelBuilder);
        }
    }
}
