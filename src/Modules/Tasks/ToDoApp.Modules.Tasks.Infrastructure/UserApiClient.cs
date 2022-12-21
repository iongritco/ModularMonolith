using ToDoApp.Modules.Tasks.Application.Clients;
using ToDoApp.Modules.Tasks.Application.Clients.DTO;
using ToDoApp.SyncBus.Interfaces;

namespace ToDoApp.Modules.Tasks.Infrastructure
{
    public class UsersApiClient : IUsersApiClient
    {
        private readonly ISyncBusClient _syncBusClient;

        public UsersApiClient(ISyncBusClient syncBusClient)
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
