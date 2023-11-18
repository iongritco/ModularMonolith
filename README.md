[![.NET](https://github.com/iongritco/ModularMonolith/actions/workflows/dotnet.yml/badge.svg)](https://github.com/iongritco/ModularMonolith/actions/workflows/dotnet.yml)

# ModularMonolith
A playground for experimenting projects with a Modular Monolith Architecture - DDD, CQRS, MediatR, .NET 8, Entity Framework Core

## Setup
- To get started, just create the databases ToDo and ToDo_Tests and update the connection string in appsettings (both in ToDoApp.Server.API and in ToDoApp.Tests.EndToEnd for integration tests) - the migration will be executed automatically on the first run. 
- Set as startup the ToDoApp.Client.Blazor and ToDoApp.Server.API projects.
