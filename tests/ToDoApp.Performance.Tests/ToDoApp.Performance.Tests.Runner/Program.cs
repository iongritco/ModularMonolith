using System.Diagnostics;
using System.Text;
using System.Text.Json;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace ToDoApp.Performance.Tests.Runner
{
    public class Program
    {
        private const string Token = "AddYourTokenHere";
        private const string BaseUrl = "https://localhost:5002";

        static void Main()
        {
            var addTaskScenario = GetAddTaskScenario();
            var getTasksScenario = GetTasksScenario();
            var getUserScenario = GetUserScenario();

            var stats = NBomberRunner
                .RegisterScenarios(addTaskScenario, getTasksScenario, getUserScenario)
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
                execute: context =>
                {
                    var request = Http.CreateRequest("GET", $"{BaseUrl}/api/users/me/name")
                        .WithHeader("Authorization", $"Bearer {Token}");

                    return Http.Send(request, context);
                });

            var getScenario = ScenarioBuilder
                .CreateScenario("get_user", getUser)
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
                execute: context =>
                {
                    var request = Http.CreateRequest("GET", $"{BaseUrl}/api/tasks")
                        .WithHeader("Authorization", $"Bearer {Token}");

                    return Http.Send(request, context);
                });

            var getScenario = ScenarioBuilder
                .CreateScenario("get_tasks", getTasksStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 20, during: TimeSpan.FromSeconds(30))
                );
            return getScenario;
        }

        private static Scenario GetAddTaskScenario()
        {
            var addTaskStep = Step.Create("add_task",
                clientFactory: HttpClientFactory.Create("addTaskClient"),
                execute: context =>
                {
                    var task = new { Id = Guid.NewGuid(), Description = "description", Username = "username" };
                    var body = JsonSerializer.Serialize(task);
                    var httpBody = new StringContent(body, Encoding.UTF8, "application/json");
                    var request = Http.CreateRequest("POST", $"{BaseUrl}/api/tasks")
                        .WithHeader("Authorization", $"Bearer {Token}")
                        .WithHeader("Content-Type", "application/json")
                        .WithBody(httpBody);

                    return Http.Send(request, context);
                });
            var addScenario = ScenarioBuilder
                .CreateScenario("add_tasks", addTaskStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 5, during: TimeSpan.FromSeconds(30))
                );
            return addScenario;
        }
    }
}