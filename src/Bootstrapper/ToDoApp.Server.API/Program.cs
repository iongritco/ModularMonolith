using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.Modules.Tasks.API;
using ToDoApp.Modules.Tasks.Persistence;
using ToDoApp.Modules.Users.API;
using ToDoApp.Modules.Users.Identity.User;

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

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>().AddEntityFrameworkStores<ToDoDataContext>().AddDefaultTokenProviders();
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
            builder.Services.AddDbContext<ToDoDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDataConnection")));
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