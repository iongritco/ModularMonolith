﻿using NetArchTest.Rules;
using System.Reflection;
using FluentAssertions;

namespace ToDoApp.Architecture.Tests;
public class ArchitectureTests : ArchitectureSetup
{
	[Fact]
	public void DomainLayer_ShouldNotDependOn_AnyOtherLayer()
	{
		// Assert
		string[] dependencies =
		[
			UsersContractsAssemblyName,
			TasksApplicationAssemblyName, TasksInfrastructureAssemblyName, TasksPersistenceAssemblyName, TasksPresentationAssemblyName,
			EmailsApplicationAssemblyName, EmailsInfrastructureAssemblyName, EmailsPresentationAssemblyName
		];

		Assembly[] assemblies = [TasksDomainAssembly, EmailsDomainAssembly];

		// Act
		var isSuccessful = Types.InAssemblies(assemblies)
			.Should()
			.NotHaveDependencyOnAny(dependencies)
			.GetResult().IsSuccessful;

		// Assert
		isSuccessful.Should().BeTrue("Domain layers should not have any dependency on other projects");
	}

    [Fact]
    public void ContractsLayer_ShouldNotDependOn_AnyOtherLayer()
    {
        // Assert
		string[] dependencies =
		[
			UsersModuleAssemblyName,
			TasksApplicationAssemblyName, TasksInfrastructureAssemblyName, TasksPersistenceAssemblyName, TasksPresentationAssemblyName, TasksDomainAssemblyName,
			EmailsApplicationAssemblyName, EmailsInfrastructureAssemblyName, EmailsPresentationAssemblyName, EmailsDomainAssemblyName
		];

        Assembly[] assemblies = [UsersContractsAssembly];

        // Act
        var isSuccessful = Types.InAssemblies(assemblies)
            .Should()
            .NotHaveDependencyOnAny(dependencies)
            .GetResult().IsSuccessful;

        // Assert
        isSuccessful.Should().BeTrue("Domain layers should not have any dependency on other projects");
    }

    [Fact]
	public void ApplicationLayer_ShouldDependOnlyOn_DomainLayer()
	{
		// Assert
		string[] dependencies =
		[
			UsersContractsAssemblyName,
            TasksInfrastructureAssemblyName, TasksPersistenceAssemblyName, TasksPresentationAssemblyName,
			EmailsInfrastructureAssemblyName, EmailsPresentationAssemblyName
		];

		Assembly[] assemblies = [TasksApplicationAssembly, EmailsApplicationAssembly];

		// Act
		var isSuccessful = Types.InAssemblies(assemblies)
			.Should()
			.NotHaveDependencyOnAny(dependencies)
			.GetResult().IsSuccessful;

		// Assert
		isSuccessful.Should().BeTrue("Application layers should depend only on Domain layers");
	}

	[Fact]
	public void InfrastructureLayer_ShouldNotDependOn_PresentationOrPersistenceLayer()
	{
		// Assert
		string[] dependencies =
		[
			TasksPersistenceAssemblyName, TasksPresentationAssemblyName,
			EmailsPresentationAssemblyName
		];

		Assembly[] assemblies = [TasksInfrastructureAssembly, EmailsInfrastructureAssembly];

		// Act
		var isSuccessful = Types.InAssemblies(assemblies)
			.Should()
			.NotHaveDependencyOnAny(dependencies)
			.GetResult().IsSuccessful;

		// Assert
		isSuccessful.Should().BeTrue("Infrastructure layers should not depend on Presentation or Persistence layers");
	}

	[Fact]
	public void UsersModule_ShouldNotDependOn_AnyOtherModuleExceptViaContracts()
	{
		// Assert
		string[] dependencies =
		[
			TasksDomainAssemblyName, TasksApplicationAssemblyName, TasksInfrastructureAssemblyName, TasksPersistenceAssemblyName, TasksPresentationAssemblyName,
			EmailsDomainAssemblyName, EmailsApplicationAssemblyName, EmailsInfrastructureAssemblyName, EmailsPresentationAssemblyName
		];

		Assembly[] assemblies = [UsersModuleAssembly];

		// Act
		var isSuccessful = Types.InAssemblies(assemblies)
			.Should()
			.NotHaveDependencyOnAny(dependencies)
			.GetResult().IsSuccessful;

		// Assert
		isSuccessful.Should().BeTrue("Users module should not have any direct dependencies to other modules");
	}

	[Fact]
	public void TasksModule_ShouldNotDependOn_AnyOtherModuleExceptViaContracts()
	{
		// Assert
		string[] dependencies =
		[
            UsersModuleAssemblyName,
            EmailsDomainAssemblyName, EmailsApplicationAssemblyName, EmailsInfrastructureAssemblyName, EmailsPresentationAssemblyName
		];

		Assembly[] assemblies = [TasksDomainAssembly, TasksApplicationAssembly, TasksInfrastructureAssembly, TasksPersistenceAssembly, TasksPresentationAssembly];

		// Act
		var isSuccessful = Types.InAssemblies(assemblies)
			.Should()
			.NotHaveDependencyOnAny(dependencies)
			.GetResult().IsSuccessful;

		// Assert
		isSuccessful.Should().BeTrue("Tasks module should not have any direct dependencies to other modules");
	}

	[Fact]
	public void EmailsModule_ShouldNotDependOn_AnyOtherModuleExceptViaContracts()
	{
		// Assert
		string[] dependencies =
		[
            UsersModuleAssemblyName,
            TasksDomainAssemblyName, TasksApplicationAssemblyName, TasksInfrastructureAssemblyName, TasksPersistenceAssemblyName, TasksPresentationAssemblyName
		];

		Assembly[] assemblies = [EmailsDomainAssembly, EmailsApplicationAssembly, EmailsInfrastructureAssembly, EmailsPresentationAssembly];

		// Act
		var isSuccessful = Types.InAssemblies(assemblies)
			.Should()
			.NotHaveDependencyOnAny(dependencies)
			.GetResult().IsSuccessful;

		// Assert
		isSuccessful.Should().BeTrue("Emails module should not have any direct dependencies to other modules");
	}
}
