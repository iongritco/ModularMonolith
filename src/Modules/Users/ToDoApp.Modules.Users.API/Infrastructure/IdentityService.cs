using Microsoft.AspNetCore.Identity;
using ToDoApp.Common.Generics;
using ToDoApp.Modules.Users.API.Infrastructure.Interfaces;
using ToDoApp.Modules.Users.API.Models;
using ToDoApp.Modules.Users.API.Models.Entities;

namespace ToDoApp.Modules.Users.API.Infrastructure;

public class IdentityService : IIdentityService
{
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly UserManager<ApplicationUser> _userManager;

	public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
	{
		_signInManager = signInManager;
		_userManager = userManager;
	}

	public async Task<bool> Authenticate(string username, string password)
	{
		var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
		return result.Succeeded;
	}

	public async Task<Result> RegisterUser(string email, string password)
	{
		var identity = new ApplicationUser { UserName = email, Email = email };
		var identityResult = await _userManager.CreateAsync(identity, password);

		return identityResult.Succeeded
				? Result.Ok()
				: Result.Fail(identityResult.Errors.Select(x => x.Description).Aggregate((a, b) => a + "; " + b));
	}

	public async Task UpdateNumberOfTasks(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);
		user.NumberOfCompletedTasks += 1;

		await _userManager.UpdateAsync(user);
	}

	public async Task<Result<User>> GetUserByEmail(string email)
	{
		var applicationUser = await _userManager.FindByEmailAsync(email);

		return applicationUser != null
			? Result.Ok(new User(applicationUser.Email, applicationUser.UserName, applicationUser.PhoneNumber))
			: Result.Fail<User>($"Unable to get user by email {email}");
	}
}
