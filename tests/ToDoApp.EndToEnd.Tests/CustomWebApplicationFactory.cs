using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Modules.Tasks.Persistence;
using ToDoApp.Modules.Users.Persistence;
using ToDoApp.Server.API;

namespace ToDoApp.Tests.EndToEnd;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            var tasksContextService = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TasksContext>));
            services.Remove(tasksContextService);                
            
            var usersContextService = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<UsersContext>));
            services.Remove(usersContextService);

            services.AddDbContext<TasksContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoConnection"),
                    builder => builder.MigrationsAssembly(typeof(TasksContext).Assembly.FullName)));


            services.AddDbContext<UsersContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoConnection"),
                    builder => builder.MigrationsAssembly(typeof(UsersContext).Assembly.FullName)));
        });
    }
}
