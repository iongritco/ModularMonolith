
using ToDoApp.Common.Generics;

namespace ToDoApp.Modules.Users.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> Authenticate(string username, string password);

        Task<Result> RegisterUser(string email, string password);

        Task UpdateNumberOfTasks(string email);
    }
}
