using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.FSharp.Core;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace ToDoApp.Performance.Tests.Runner
{
    public class Program
    {
        private static string? _token;
        private static string? _username;
        private static string? _password;
        private static string? _host;

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
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, htmlReport?.Substring(2) ?? string.Empty);
            Process.Start(@"cmd.exe ", @"/c " + fullPath);

            Console.ReadKey();
        }

        private static Scenario GetUserScenario()
        {
            var getUser = Step.Create("get_user",
                clientFactory: HttpClientFactory.Create("getUserClient"),
                execute: async context =>
                {
                    if (string.IsNullOrEmpty(_token))
                    {
                        await SetToken(context);
                    }
                    var request = Http.CreateRequest("GET", $"{_host}/api/users/me/name")
                        .WithHeader("Authorization", $"Bearer {_token}");

                    return await Http.Send(request, context);
                });

            var getScenario = ScenarioBuilder
                .CreateScenario("get_user", getUser)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 5, during: TimeSpan.FromSeconds(30))
                );
            return getScenario;
        }

        private static Scenario GetTokeScenario()
        {
            var getToken = Step.Create("get_token",
                clientFactory: HttpClientFactory.Create("getTokenClient"),
                execute: SetToken);

            var getScenario = ScenarioBuilder
                .CreateScenario("get_token", getToken)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 5, during: TimeSpan.FromSeconds(30))
                );
            return getScenario;
        }

        private static Scenario GetTasksScenario()
        {
            var getTasksStep = Step.Create("get_tasks",
                clientFactory: HttpClientFactory.Create("getTasksClient"),
                execute: async context =>
                {
                    if (string.IsNullOrEmpty(_token))
                    {
                        await SetToken(context);
                    }

                    var request = Http.CreateRequest("GET", $"{_host}/api/tasks")
                        .WithHeader("Authorization", $"Bearer {_token}");

                    return await Http.Send(request, context);
                });

            var getScenario = ScenarioBuilder
                .CreateScenario("get_tasks", getTasksStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 20, during: TimeSpan.FromSeconds(30))
                );
            return getScenario;
        }

        private static Scenario AddTaskScenario()
        {
            var addTaskStep = Step.Create("add_task",
                clientFactory: HttpClientFactory.Create("addTaskClient"),
                execute: async context =>
                {
                    if (string.IsNullOrEmpty(_token))
                    {
                        await SetToken(context);
                    }

                    var task = new { Id = Guid.NewGuid(), Description = "description", Username = "username" };
                    var body = JsonSerializer.Serialize(task);
                    var httpBody = new StringContent(body, Encoding.UTF8, "application/json");
                    var request = Http.CreateRequest("POST", $"{_host}/api/tasks")
                        .WithHeader("Authorization", $"Bearer {_token}")
                        .WithHeader("Content-Type", "application/json")
                        .WithBody(httpBody);

                    return await Http.Send(request, context);
                });
            var addScenario = ScenarioBuilder
                .CreateScenario("add_tasks", addTaskStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 5, during: TimeSpan.FromSeconds(30))
                );
            return addScenario;
        }

        private static async Task<Response> SetToken(IStepContext<HttpClient, Unit> context)
        {
            var user = new { Username = _username, Password = _password };
            var body = JsonSerializer.Serialize(user);
            var httpBody = new StringContent(body, Encoding.UTF8, "application/json");
            var request = Http.CreateRequest("POST", $"{_host}/api/users/login")
                .WithHeader("Content-Type", "application/json")
                .WithBody(httpBody);

            var result = await Http.Send(request, context);
            var isSuccessful = result.StatusCode == (int?)HttpStatusCode.OK;
            if (isSuccessful)
            {
                var content = result.Payload as HttpResponseMessage;
                var token = await content?.Content.ReadAsStringAsync()!;
                if (string.IsNullOrEmpty(_token))
                {
                    _token = token;
                }
            }

            return result;
        }
    }
}