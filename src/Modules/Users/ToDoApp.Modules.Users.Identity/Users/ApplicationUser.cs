
using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Modules.Users.Identity.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int NumberOfCompletedTasks { get; set; }
    }
}
