using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.EventBus.MassTransit;
using ToDoApp.Modules.Tasks.API;
using ToDoApp.Modules.Users.API;
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
            
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationHandler<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceHandler<,>));

            builder.Services.AddMassTransit();

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

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}