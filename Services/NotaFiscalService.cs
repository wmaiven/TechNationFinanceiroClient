using Microsoft.JSInterop;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TechNationFinanceiroClient.Models;
using TechNationFinanceiroClient.Services.Interfaces;
using Newtonsoft.Json.Linq;
public class NotaFiscalService : INotaFiscalService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public NotaFiscalService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<IEnumerable<NotaFiscal>> GetNotasFiscais()
    {
        var token = await GetTokenFromLocalStorage();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync("NotasFiscais");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<NotaFiscal>>();
    }

    public async Task<NotaFiscal> GetNotaFiscal(int id)
    {
        var token = await GetTokenFromLocalStorage();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"NotasFiscais/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NotaFiscal>();
    }

    public async Task<NotaFiscal> PostNotaFiscal(NotaFiscal notaFiscal)
    {
        var token = await GetTokenFromLocalStorage();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync("NotasFiscais", notaFiscal);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NotaFiscal>();
    }

    public async Task<bool> PutNotaFiscal(int id, NotaFiscal notaFiscal)
    {
        var token = await GetTokenFromLocalStorage();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync($"NotasFiscais/{id}", notaFiscal);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteNotaFiscal(int id)
    {
        var token = await GetTokenFromLocalStorage();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync($"NotasFiscais/{id}");
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    private async Task<string> GetTokenFromLocalStorage()
    {
        var tokenString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (string.IsNullOrWhiteSpace(tokenString))
            return null;

        try
        {
            // Parse o token JSON para um objeto usando Newtonsoft.Json ou System.Text.Json
            var tokenObject = JObject.Parse(tokenString);

            // Extrai o valor do token JWT
            var jwtToken = tokenObject["token"].ToString();

            return jwtToken;
        }
        catch (Exception ex)
        {
            // Lida com qualquer erro de parsing ou de token inválido
            Console.WriteLine($"Erro ao extrair token JWT: {ex.Message}");
            return null;
        }
    }

}

