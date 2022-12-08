using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.Modules.Tasks.Api
{
    public static class TasksModule
    {
        public static IServiceCollection AddTasksModule(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseTasksModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}