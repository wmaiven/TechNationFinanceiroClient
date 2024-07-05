using System.Net.Http.Json;
using TechNationFinanceiroApi.Models;

public class NotaFiscalService : INotaFiscalService
{
    private readonly HttpClient _httpClient;

    public NotaFiscalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<NotaFiscal>> GetNotasFiscais()
    {
        var response = await _httpClient.GetAsync("NotasFiscais");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<NotaFiscal>>();
    }

    public async Task<NotaFiscal> GetNotaFiscal(int id)
    {
        var response = await _httpClient.GetAsync($"NotasFiscais/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NotaFiscal>();
    }

    public async Task<NotaFiscal> PostNotaFiscal(NotaFiscal notaFiscal)
    {
        var response = await _httpClient.PostAsJsonAsync("NotasFiscais", notaFiscal);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<NotaFiscal>();
    }

    public async Task<bool> PutNotaFiscal(int id, NotaFiscal notaFiscal)
    {
        var response = await _httpClient.PutAsJsonAsync($"NotasFiscais/{id}", notaFiscal);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteNotaFiscal(int id)
    {
        var response = await _httpClient.DeleteAsync($"NotasFiscais/{id}");
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }
}
