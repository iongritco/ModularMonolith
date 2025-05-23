using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using Xunit;
using System.Net.Http.Headers;

namespace ToDoApp.Tests.EndToEnd;

public class BaseTestsClass : IAsyncLifetime
{
    public static string Username = "test@test.com";
    public static HttpClient CustomHttpClient;
    private readonly string _dbConnection;
    private Respawner _respawner;

    public BaseTestsClass()
    {
        var applicationFactory = new CustomWebApplicationFactory();
        var configuration = applicationFactory.Services.GetRequiredService<IConfiguration>();
        _dbConnection = configuration.GetConnectionString("ToDoConnection");
        CustomHttpClient = applicationFactory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        _respawner = await Respawner.CreateAsync(_dbConnection,
            new RespawnerOptions
            {
                TablesToIgnore = new Table[]
                {
                    "__EFMigrationsHistory",
                }
            });

        const string password = "$tr0ngP@ssword";

        var userRegistrationResult = await CustomHttpClient.PostAsJsonAsync("api/users/register", new { Email = Username, Password = password });
        if (!userRegistrationResult.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }

        var tokenResult = await CustomHttpClient.PostAsJsonAsync("api/users/login", new { Username = Username, Password = password });
        if (!tokenResult.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }

        var token = await tokenResult.Content.ReadAsStringAsync();
        CustomHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task DisposeAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
}
