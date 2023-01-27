using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using ToDoApp.Client.Blazor.ViewModels;

namespace ToDoApp.Client.Blazor.Pages
{
    public partial class ToDo
    {
        private List<TaskModel> _toDoList = new();
        private string? _newItem;
        private string? _currentUser;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                _currentUser = user.Identity.Name;
                _toDoList = await HttpClient.GetFromJsonAsync<List<TaskModel>>("api/tasks");
            }
            else
            {
                NavigationManager.NavigateTo("login/");
            }
        }

        private async Task AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(_newItem))
            {
                var todo = new TaskModel(Guid.NewGuid(), _newItem, _currentUser, DateTime.Today, Status.ToDo);
                _toDoList?.Add(todo);
                await HttpClient.PostAsJsonAsync("api/tasks", todo);
                _newItem = string.Empty;
            }
        }

        private void RemoveTodo(Guid guid)
        {
            _toDoList?.Remove(_toDoList.First(x => x.Id.Equals(guid)));
            HttpClient.DeleteAsync($"api/tasks/{guid}");
        }

        private async Task StartTodo(Guid guid)
        {
            await UpdateTodo(guid, Status.InProgress);
        }

        private async Task CompleteTodo(Guid guid)
        {
            await UpdateTodo(guid, Status.Completed);
        }

        private async Task UpdateTodo(Guid guid, Status status)
        {
            var todo = _toDoList.First(x => x.Id.Equals(guid));
            todo.Status = status;
            await HttpClient.PutAsJsonAsync("api/tasks", todo);
        }
    }
}
