using System.Net.Http.Json;
using TechNationFinanceiroClient.Models;
using TechNationFinanceiroClient.Services.Interfaces;

namespace TechNationFinanceiroClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("User/login", user);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> Logout()
        { 
            return true;
        }
    }
}