﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ToDoApp.Client.Blazor.ViewModels;

namespace ToDoApp.Client.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<string> Register(RegisterModel registerModel)
    {
        var postTask = await _httpClient.PostAsJsonAsync("api/users/register", new { Email = registerModel.Email, Password = registerModel.Password });
        var result = await postTask.Content.ReadAsStringAsync();
        return result;
    }

    public async Task<string> Login(LoginModel loginModel)
    {
        var tokenTask = await _httpClient.PostAsJsonAsync("api/users/login", new { Username = loginModel.Email, Password = loginModel.Password });
        var token = await tokenTask.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(token))
        {
            return token;
        }

        await _localStorage.SetItemAsync("authToken", token);
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return token;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}