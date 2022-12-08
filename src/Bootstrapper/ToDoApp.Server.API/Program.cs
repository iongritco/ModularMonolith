using ToDoApp.Modules.Tasks.Api;
using ToDoApp.Modules.Users.Api;

namespace ToDoApp.Server.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add modules
            builder.Services.AddTasksModule();
            builder.Services.AddUsersModule();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Use modules
            app.UseTasksModule();
            app.UseUsersModule();


            app.MapControllers();

            app.Run();
        }
    }
}