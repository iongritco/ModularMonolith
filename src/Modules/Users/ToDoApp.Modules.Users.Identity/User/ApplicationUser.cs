
using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Modules.Users.Identity.User
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int NumberOfCompletedTasks { get; set; }
    }
}
