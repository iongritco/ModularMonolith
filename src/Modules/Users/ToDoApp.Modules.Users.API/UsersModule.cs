using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Modules.Users.Application.Interfaces;
using ToDoApp.Modules.Users.Identity.JwtToken;
using ToDoApp.Modules.Users.Identity.User;

namespace ToDoApp.Modules.Users.API
{
    public static class UsersModule
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            services.AddSingleton<ISettings, Settings>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            return services;
        }

        public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}