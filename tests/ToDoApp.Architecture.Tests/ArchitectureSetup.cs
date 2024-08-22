using System.Reflection;
using ToDoApp.Modules.Emails.API;
using ToDoApp.Modules.Emails.Application.Interfaces;
using ToDoApp.Modules.Emails.Domain.Entities;
using ToDoApp.Modules.Emails.Infrastructure;
using ToDoApp.Modules.Tasks.API;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;
using ToDoApp.Modules.Tasks.Infrastructure;
using ToDoApp.Modules.Tasks.Persistence.ToDo;
using ToDoApp.Modules.Users.API;
using ToDoApp.Modules.Users.Application.Interfaces;
using ToDoApp.Modules.Users.Domain.Entities;
using ToDoApp.Modules.Users.Identity.Users;
using ToDoApp.Modules.Users.Persistence;

namespace ToDoApp.Architecture.Tests;

public class ArchitectureSetup
{
	// Users module
	protected static readonly Assembly UsersDomainAssembly = typeof(User).Assembly;
	protected static readonly Assembly UsersApplicationAssembly = typeof(IIdentityService).Assembly;
	protected static readonly Assembly UsersIdentityAssembly = typeof(IdentityService).Assembly;
	protected static readonly Assembly UsersPersistenceAssembly = typeof(UsersContext).Assembly;
	protected static readonly Assembly UsersPresentationAssembly = typeof(UsersModule).Assembly;
	protected static readonly string? UsersDomainAssemblyName = UsersDomainAssembly.GetName().Name;
	protected static readonly string? UsersApplicationAssemblyName = UsersApplicationAssembly.GetName().Name;
	protected static readonly string? UsersIdentityAssemblyName = UsersIdentityAssembly.GetName().Name;
	protected static readonly string? UsersPersistenceAssemblyName = UsersPersistenceAssembly.GetName().Name;
	protected static readonly string? UsersPresentationAssemblyName = UsersPresentationAssembly.GetName().Name;
	
	// Tasks module
	protected static readonly Assembly TasksDomainAssembly = typeof(ToDoItem).Assembly;
	protected static readonly Assembly TasksApplicationAssembly = typeof(ITasksCommandRepository).Assembly;
	protected static readonly Assembly TasksInfrastructureAssembly = typeof(UsersApiClient).Assembly;
	protected static readonly Assembly TasksPersistenceAssembly = typeof(TasksCommandRepository).Assembly;
	protected static readonly Assembly TasksPresentationAssembly = typeof(TasksModule).Assembly;
	protected static readonly string? TasksDomainAssemblyName = TasksDomainAssembly.GetName().Name;
	protected static readonly string? TasksApplicationAssemblyName = TasksApplicationAssembly.GetName().Name;
	protected static readonly string? TasksInfrastructureAssemblyName = TasksInfrastructureAssembly.GetName().Name;
	protected static readonly string? TasksPersistenceAssemblyName = TasksPersistenceAssembly.GetName().Name;
	protected static readonly string? TasksPresentationAssemblyName = TasksPresentationAssembly.GetName().Name;
	
	// Emails module
	protected static readonly Assembly EmailsDomainAssembly = typeof(Email).Assembly;
	protected static readonly Assembly EmailsApplicationAssembly = typeof(IEmailService).Assembly;
	protected static readonly Assembly EmailsInfrastructureAssembly = typeof(EmailService).Assembly;
	protected static readonly Assembly EmailsPresentationAssembly = typeof(EmailsModule).Assembly;
	protected static readonly string? EmailsDomainAssemblyName = EmailsDomainAssembly.GetName().Name;
	protected static readonly string? EmailsApplicationAssemblyName = EmailsApplicationAssembly.GetName().Name;
	protected static readonly string? EmailsInfrastructureAssemblyName = EmailsInfrastructureAssembly.GetName().Name;
	protected static readonly string? EmailsPresentationAssemblyName = EmailsPresentationAssembly.GetName().Name; 
}