
using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Modules.Users.API.Domain;

public class ApplicationUser : IdentityUser<Guid>
{
	public int NumberOfCompletedTasks { get; set; }
}
