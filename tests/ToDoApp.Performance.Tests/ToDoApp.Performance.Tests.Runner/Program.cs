using Microsoft.Extensions.Configuration;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Http.CSharp;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ToDoApp.Performance.Tests.Runner;

public class Program
{
    private static string? _username;
    private static string? _password;
    private static string? _host;
    private static string? _token;

    static void Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        var config = builder.Build();
        _username = config["Username"];
        _password = config["Password"];
        _host = config["Host"];

        var getTokenScenario = GetTokeScenario();
        var addTaskScenario = AddTaskScenario();
        var getTasksScenario = GetTasksScenario();
        var getUserScenario = GetUserScenario();

        var stats = NBomberRunner
            .RegisterScenarios(
                getTokenScenario,
                addTaskScenario,
                getTasksScenario,
                getUserScenario)
            .Run();

        var htmlReport = stats.ReportFiles.SingleOrDefault(x => x.ReportFormat.Equals(ReportFormat.Html))?.FilePath;
        var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, htmlReport);
        Process.Start(@"cmd.exe ", @"/c " + fullPath);

        Console.ReadKey();
    }

    private static ScenarioProps GetUserScenario()
    {
        var httpClient = Http.CreateDefaultClient();
        var getScenario = Scenario
            .Create("get_user", async context =>
            {
                var request = Http.CreateRequest("GET", $"{_host}/api/users/me/name")
                    .WithHeader("Authorization", $"Bearer {_token}");

                return await Http.Send(httpClient, request);
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(Simulation.Inject(rate: 5, TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)))
            .WithInit(async ctx =>
            {
                await SetToken(ctx, httpClient);
            });

        return getScenario;
    }

    private static ScenarioProps GetTokeScenario()
    {
        var httpClient = Http.CreateDefaultClient();
        var getTokenScenario = Scenario
            .Create("get_token", async context =>
            {
                var user = new { Username = _username, Password = _password };
                var body = JsonSerializer.Serialize(user);
                var httpBody = new StringContent(body, Encoding.UTF8, "application/json");
                var request = Http.CreateRequest("POST", $"{_host}/api/users/login")
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(httpBody);

                return await Http.Send(httpClient, request);
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(Simulation.Inject(rate: 5, TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)))
            .WithInit(async ctx =>
            {
                await SetToken(ctx, httpClient);
            });

        return getTokenScenario;
    }

    private static ScenarioProps GetTasksScenario()
    {
        var httpClient = Http.CreateDefaultClient();
        var getScenario = Scenario
            .Create("get_tasks", async context =>
            {
                var request = Http.CreateRequest("GET", $"{_host}/api/tasks")
                    .WithHeader("Authorization", $"Bearer {_token}");

                return await Http.Send(httpClient, request);
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(Simulation.Inject(rate: 20, TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)))
            .WithInit(async ctx =>
            {
                await SetToken(ctx, httpClient);
            });

        return getScenario;
    }

    private static ScenarioProps AddTaskScenario()
    {
        var httpClient = Http.CreateDefaultClient();
        var getScenario = Scenario
            .Create("add_task", async context =>
            {
                var task = new { Id = Guid.NewGuid(), Description = "description", Username = "username" };
                var body = JsonSerializer.Serialize(task);
                var httpBody = new StringContent(body, Encoding.UTF8, "application/json");
                var request = Http.CreateRequest("POST", $"{_host}/api/tasks")
                    .WithHeader("Authorization", $"Bearer {_token}")
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(httpBody);

                return await Http.Send(httpClient, request);
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(Simulation.Inject(rate: 5, TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30)))
            .WithInit(async ctx =>
            {
                await SetToken(ctx, httpClient);
            }); ;

        return getScenario;
    }

    private static async Task SetToken(IScenarioInitContext context, HttpClient httpClient)
    {
        var user = new { Username = _username, Password = _password };
        var body = JsonSerializer.Serialize(user);
        var httpBody = new StringContent(body, Encoding.UTF8, "application/json");
        var request = Http.CreateRequest("POST", $"{_host}/api/users/login")
            .WithHeader("Content-Type", "application/json")
            .WithBody(httpBody);

        var tokenRequest = await Http.Send(httpClient, request);
        if (tokenRequest.StatusCode == HttpStatusCode.OK.ToString())
        {
            var responseValue = tokenRequest.Payload.Value;
            _token = await responseValue.Content.ReadAsStringAsync();
        }
    }
}