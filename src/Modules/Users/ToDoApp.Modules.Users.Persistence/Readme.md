Script for adding a new migration (update paths based on your needs):
dotnet ef migrations add InitialCreate --verbose --project "C:\CSV\Projects\ModularMonolith\src\Modules\Users\ToDoApp.Modules.Users.Persistence\ToDoApp.Modules.Users.Persistence.csproj" --startup-project "C:\CSV\Projects\ModularMonolith\src\Bootstrapper\ToDoApp.Server.API\ToDoApp.Server.API.csproj" --context UsersContext