using System.Reflection;

using FluentValidation;

using HealthChecks.UI.Client;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using ToDoApp.Common;
using ToDoApp.EventBus.MassTransit;
using ToDoApp.Modules.Emails.API;
using ToDoApp.Modules.Emails.Application.Commands.SendEmail;
using ToDoApp.Modules.Tasks.API;
using ToDoApp.Modules.Tasks.Application.Queries;
using ToDoApp.Modules.Users.API;
using ToDoApp.Modules.Users.Application.Queries.GetToken;
using ToDoApp.Server.API.Pipelines;
using ToDoApp.SyncBus;

namespace ToDoApp.Server.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add health checks
            builder.Services
                .AddHealthChecks()
                .AddSqlServer(builder.Configuration["ConnectionStrings:ToDoConnection"], healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Database" });
            builder.Services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint("TODO API", "/api/health"); //map health check api    

            }).AddInMemoryStorage();

            // Add modules
            builder.Services.AddTasksModule(builder.Configuration);
            builder.Services.AddUsersModule(builder.Configuration);
            builder.Services.AddEmailsModule(builder.Configuration);

            builder.Services.AddCommonServices();
            builder.Services.AddSyncBus();
            builder.Services.AddMassTransit(typeof(TasksModule).GetTypeInfo().Assembly, typeof(UsersModule).GetTypeInfo().Assembly, typeof(EmailsModule).GetTypeInfo().Assembly);

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationHandler<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceHandler<,>));

            var tasksModuleAssembly = typeof(GetTasksQuery).GetTypeInfo().Assembly;
            var usersModuleAssembly = typeof(GetTokenQuery).GetTypeInfo().Assembly;
            var emailModuleAssembly = typeof(SendEmailCommand).GetTypeInfo().Assembly;
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(tasksModuleAssembly, usersModuleAssembly, emailModuleAssembly));
            builder.Services.AddValidatorsFromAssemblies(new[] { tasksModuleAssembly, usersModuleAssembly, emailModuleAssembly });

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

            app.MapHealthChecks("/api/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.MapHealthChecksUI();

            app.Run();
        }
    }
}