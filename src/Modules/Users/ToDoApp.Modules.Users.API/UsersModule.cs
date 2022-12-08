using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.Modules.Users.Api
{
    public static class UsersModule
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}