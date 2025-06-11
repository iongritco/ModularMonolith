
using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Modules.Users.API.Models;

public class ApplicationUser : IdentityUser<Guid>
{
	public int NumberOfCompletedTasks { get; set; }
}
