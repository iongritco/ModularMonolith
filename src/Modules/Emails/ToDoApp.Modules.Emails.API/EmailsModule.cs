using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ToDoApp.Modules.Emails.API.Configurations;
using ToDoApp.Modules.Emails.Application.Interfaces;
using ToDoApp.Modules.Emails.Infrastructure;
using ToDoApp.Modules.Emails.Infrastructure.Interfaces;

namespace ToDoApp.Modules.Emails.API
{
    public static class EmailsModule
    {
        public static IServiceCollection AddEmailsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();

            services.AddSingleton<IEmailConfigurations>(
                _ =>
                {
                    var configurations = new EmailConfigurations();
                    configuration.GetSection("EmailConfigurations").Bind(configurations);
                    return configurations;
                });
            return services;
        }

        public static IApplicationBuilder UseEmailsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
