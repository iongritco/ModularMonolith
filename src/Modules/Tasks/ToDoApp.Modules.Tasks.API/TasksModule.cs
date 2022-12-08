using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Application.Queries;
using ToDoApp.Modules.Tasks.Persistence.ToDo;
using MediatR;

namespace ToDoApp.Modules.Tasks.API
{
    public static class TasksModule
    {
        public static IServiceCollection AddTasksModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetTasksQuery).GetTypeInfo().Assembly);
            services.AddValidatorsFromAssembly(typeof(GetTasksQuery).GetTypeInfo().Assembly);
            services.AddTransient<IToDoQueryRepository, ToDoQueryRepository>();
            services.AddTransient<IToDoCommandRepository, ToDoCommandRepository>();
            return services;
        }

        public static IApplicationBuilder UseTasksModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}