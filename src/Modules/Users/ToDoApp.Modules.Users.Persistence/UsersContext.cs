using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Modules.Users.Identity.Users;

namespace ToDoApp.Modules.Users.Persistence;

public class UsersContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public UsersContext(DbContextOptions<UsersContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        base.OnModelCreating(modelBuilder);
    }
}