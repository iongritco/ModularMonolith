using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Persistence.ToDo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoApp.Modules.Tasks.Persistence;

namespace ToDoApp.Modules.Tasks.API
{
    public static class TasksModule
    {
        public static IServiceCollection AddTasksModule(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddTransient<ITasksQueryRepository, TasksQueryRepository>();
            services.AddTransient<ITasksCommandRepository, TasksCommandRepository>();
            services.AddDbContext<TasksContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ToDoConnection")));

            return services;
        }

        public static IApplicationBuilder UseTasksModule(this IApplicationBuilder app)
        {
            InitializeDatabase(app);
            return app;
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<TasksContext>().Database.Migrate();
            }
        }
    }
}