[![.NET](https://github.com/iongritco/ModularMonolith/actions/workflows/dotnet.yml/badge.svg)](https://github.com/iongritco/ModularMonolith/actions/workflows/dotnet.yml)

# ModularMonolith
A playground for experimenting projects with a Modular Monolith Architecture - DDD, CQRS, MediatR, .NET 8, Entity Framework Core

## Setup
- To get started, just create the databases ToDo and ToDo_Tests and update the connection string in appsettings (both in ToDoApp.Server.API and in ToDoApp.Tests.EndToEnd for integration tests) - the migration will be executed automatically on the first run. 
- Set as startup the ToDoApp.Client.Blazor and ToDoApp.Server.API projects.

### Health checks
Server has both endpoint and UI for checking the health of dependencies, in this case SQL Server and MassTransit:
- Endpoint - default address: https://localhost:5002/api/health
- UI - default address: https://localhost:5002/healthchecks-ui

### Modules communications
There are two ways of communicating between modules:
1) Asynchronous - via MassTransit that runs locally
2) Synchronous - in two ways, you can check TasksModule for reference, CreateToDoCommandHandler command and implementations for IUsersApiClient:
    - via Contracts projects - direct dependency on Contracts project without direct dependency on any other module component
    - via SyncBus - dynamically by providing a route that runs dynamically the request, no direct dependency
    


## Credits
- This implementation is based on the original implementation from https://github.com/devmentors/ModularMonolith and https://github.com/kgrzybek/modular-monolith-with-ddd
