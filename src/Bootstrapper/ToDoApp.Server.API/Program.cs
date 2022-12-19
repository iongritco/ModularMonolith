using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.EventBus.MassTransit;
using ToDoApp.Modules.Emails.API;
using ToDoApp.Modules.Emails.Application.Commands.SendEmail;
using ToDoApp.Modules.Tasks.API;
using ToDoApp.Modules.Tasks.Application.Queries;
using ToDoApp.Modules.Users.API;
using ToDoApp.Modules.Users.Application.Queries.GetToken;
using ToDoApp.Server.API.Pipelines;

namespace ToDoApp.Server.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add modules
            builder.Services.AddTasksModule(builder.Configuration);
            builder.Services.AddUsersModule(builder.Configuration);
            builder.Services.AddEmailsModule();
            
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationHandler<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceHandler<,>));

            var tasksModuleAssembly = typeof(GetTasksQuery).GetTypeInfo().Assembly;
            var usersModuleAssembly = typeof(GetTokenQuery).GetTypeInfo().Assembly;
            var emailModuleAssembly = typeof(SendEmailCommand).GetTypeInfo().Assembly;
            builder.Services.AddMediatR(tasksModuleAssembly, usersModuleAssembly, emailModuleAssembly);
            builder.Services.AddValidatorsFromAssemblies(new[] { tasksModuleAssembly, usersModuleAssembly, emailModuleAssembly });

            builder.Services.AddMassTransit(typeof(TasksModule).GetTypeInfo().Assembly, typeof(UsersModule).GetTypeInfo().Assembly, typeof(EmailsModule).GetTypeInfo().Assembly);

            builder.Services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(
                    options =>
                    {
                        var signingKey = Convert.FromBase64String(builder.Configuration["JwtSecret"]);
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(signingKey)
                        };
                    });
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(corsPolicyBuilder => corsPolicyBuilder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseHttpsRedirection();

            // Use modules
            app.UseTasksModule();
            app.UseUsersModule();
            app.UseEmailsModule();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}