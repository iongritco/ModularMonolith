using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Modules.Emails.Application.Interfaces;
using ToDoApp.Modules.Emails.Infrastructure;

namespace ToDoApp.Modules.Emails.API
{
    public static class EmailsModule
    {
        public static IServiceCollection AddEmailsModule(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        public static IApplicationBuilder UseEmailsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
