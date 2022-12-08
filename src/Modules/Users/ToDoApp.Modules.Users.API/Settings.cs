using Microsoft.Extensions.Configuration;
using ToDoApp.Modules.Users.Application.Interfaces;

namespace ToDoApp.Modules.Users.API
{
    public class Settings : ISettings
    {
        private readonly IConfiguration _configuration;

        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenKey { get => _configuration["JwtSecret"]; }
    }
}
