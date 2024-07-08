using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop; // Adicione este using
using TechNationFinanceiroClient.Models;
using TechNationFinanceiroClient.Services.Interfaces;

namespace TechNationFinanceiroClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime; // Adicione esta linha

        public AuthenticationService(HttpClient httpClient, NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime; // Injete o IJSRuntime
        }

        public async Task<string> GetTokenAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("User/login", user);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task Logout()
        {
            // Remove o token do localStorage usando JavaScript interop
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");

            // Redireciona para a página de login
            _navigationManager.NavigateTo("/");
        }

        public async Task<bool> IsAuthenticated()
        {
            // Verifica se existe algum token no localStorage usando JavaScript interop
            var result = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            return !string.IsNullOrEmpty(result);
        }
    }
}
