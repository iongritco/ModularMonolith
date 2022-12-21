using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Common.Interfaces;

namespace ToDoApp.Common
{
    public static class Extensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<ISerializer, JsonSerializer>();

            return services;
        }
    }
}
