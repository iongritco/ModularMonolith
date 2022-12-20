﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Modules.Users.Application.Interfaces;
using ToDoApp.Modules.Users.Identity.JwtToken;
using ToDoApp.Modules.Users.Identity.Users;
using ToDoApp.Modules.Users.Persistence;

namespace ToDoApp.Modules.Users.API
{
    public static class UsersModule
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<ISettings, Settings>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<UsersContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<UsersContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ToDoConnection")));

            return services;
        }

        public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
        {
            InitializeDatabase(app);
            return app;
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<UsersContext>().Database.Migrate();
            }
        }
    }
}