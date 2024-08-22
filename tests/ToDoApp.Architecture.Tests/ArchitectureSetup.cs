using System.Reflection;
using ToDoApp.Modules.Users.API;
using ToDoApp.Modules.Users.Application.Interfaces;
using ToDoApp.Modules.Users.Domain.Entities;
using ToDoApp.Modules.Users.Persistence;

namespace ToDoApp.Architecture.Tests;

public class ArchitectureSetup
{
	// Users module
	protected static readonly Assembly UsersDomainAssembly = typeof(User).Assembly;
	protected static readonly Assembly UsersApplicationAssembly = typeof(IIdentityService).Assembly;
	protected static readonly Assembly UsersIdentityAssembly = typeof(IIdentityService).Assembly;
	protected static readonly Assembly UsersPersistenceAssembly = typeof(UsersContext).Assembly;
	protected static readonly Assembly UsersPresentationAssembly = typeof(UsersModule).Assembly;
	protected static readonly string? UsersDomainAssemblyName = UsersDomainAssembly.GetName().Name;
	protected static readonly string? UsersApplicationAssemblyName = UsersApplicationAssembly.GetName().Name;
	protected static readonly string? UsersIdentityAssemblyName = UsersIdentityAssembly.GetName().Name;
	protected static readonly string? UsersPersistenceAssemblyName = UsersPersistenceAssembly.GetName().Name;
	protected static readonly string? UsersPresentationAssemblyName = UsersPresentationAssembly.GetName().Name; 
}