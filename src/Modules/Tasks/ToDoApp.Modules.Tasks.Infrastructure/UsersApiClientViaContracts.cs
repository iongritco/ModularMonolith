using Mapster;

using ToDoApp.Modules.Tasks.Application.Clients;
using ToDoApp.Modules.Tasks.Application.Clients.DTOs;
using ToDoApp.Modules.Users.Contracts;

namespace ToDoApp.Modules.Tasks.Infrastructure;

// Static implementation via Contracts projects
public class UsersApiClientViaContracts : IUsersApiClient
{
    private readonly IUsersModuleService _usersModuleService;

    public UsersApiClientViaContracts(IUsersModuleService usersModuleService)
    {
        _usersModuleService = usersModuleService;
    }

    public async Task<UserDto> GetUser(string email)
    {
        var user = await _usersModuleService.GetUser(email);
        return user.Adapt<UserDto>();
    }
}
