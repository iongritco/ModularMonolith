using ToDoApp.Modules.Tasks.Application.Clients;
using ToDoApp.Modules.Tasks.Application.Clients.DTOs;
using ToDoApp.SyncBus.Interfaces;

namespace ToDoApp.Modules.Tasks.Infrastructure
{
    // Dynamic implementation via "syncBusClient"
    public class UsersApiClientViaSyncBus : IUsersApiClient
    {
        private readonly ISyncBusClient _syncBusClient;

        public UsersApiClientViaSyncBus(ISyncBusClient syncBusClient)
        {
            _syncBusClient = syncBusClient;
        }

        public async Task<UserDto> GetUser(string email)
        {
            var result = await _syncBusClient.SendAsync<UserDto>("getUserByEmail", new { email });
            return result;
        }
    }
}
