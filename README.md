[![.NET](https://github.com/iongritco/ModularMonolith/actions/workflows/dotnet.yml/badge.svg)](https://github.com/iongritco/ModularMonolith/actions/workflows/dotnet.yml)

# ModularMonolith
A playground for experimenting projects with a Modular Monolith Architecture - DDD, CQRS, MediatR, .NET 9, Entity Framework Core

## Setup
- To get started, just create the databases ToDo and ToDo_Tests and update the connection string in appsettings (both in ToDoApp.Server.API and in ToDoApp.Tests.EndToEnd for integration tests) - the migration will be executed automatically on the first run. 
- Set as startup the ToDoApp.Client.Blazor and ToDoApp.Server.API projects.

### Tests
#### Unit tests
- Unit tests can be executed directly from Visual Studio, no additional setup. Every module has it's own set of unit tests that can be found under Modules\[ModuleName]\Tests
#### Architecture tests
- The scope of architecture tests \tests\ToDoApp.Architecture.Tests is to enforce the dependency checks between modules and between layers in a specific module
#### End to end tests
- The scope of the end to end tests is to simulate an end to end flow by using WebApplicationFactory and an actual database
#### Performance tests
- There are two types of performance tests - by using k6 and nbomber
- To run stress tests with **k6**, you need first to install k6 by following [these steps](https://grafana.com/docs/k6/latest/set-up/install-k6/). After that, update the .\tests\ToDoApp.Performance.Tests\scripts.js file with the url, username and password that you want to use. Then run the application, open a cmd in the .\tests\ToDoApp.Performance.Tests folder and execute *k6 run script.js*
- To run the tests with nbomber, open the solution .\tests\ToDoApp.Performance.Tests\ToDoApp.Performance.Tests.sln, update the username and password of the user in appsettings.json. Run the application and after that run the performance tests as well. At the end, you'll get a report with all the results.
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
- This implementation is based on the original ideas from [devmentors](https://github.com/devmentors/ModularMonolith) and [kgrzybek](https://github.com/kgrzybek/modular-monolith-with-ddd)
