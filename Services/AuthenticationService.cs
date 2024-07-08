using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using TechNationFinanceiroClient.Models;
using TechNationFinanceiroClient.Services.Interfaces;

namespace TechNationFinanceiroClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime;

        public AuthenticationService(HttpClient httpClient, NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetTokenAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("User/login", user);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseString);
            var token = jsonObject["token"].ToString();
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
            return token;
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            _navigationManager.NavigateTo("/login");
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string> GetTokenFromLocalStorage()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            return token;
        }
    }
}
