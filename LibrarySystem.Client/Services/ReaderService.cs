namespace LibrarySystem.Client.Services;
using System.Net.Http.Json;
using LibrarySystem.Client.Models;

public class ReaderService
{
    private readonly HttpClient _http;
    public ReaderService(HttpClient http) => _http = http;

    public async Task<List<Reader>> GetReadersAsync() => 
        await _http.GetFromJsonAsync<List<Reader>>("api/readers") ?? new();

    public async Task AddReaderAsync(Reader reader) => 
        await _http.PostAsJsonAsync("api/readers", reader);

    public async Task UpdateReaderAsync(int id, Reader reader) => 
        await _http.PutAsJsonAsync($"api/readers/{id}", reader);

    public async Task DeleteReaderAsync(int id) => 
        await _http.DeleteAsync($"api/readers/{id}");
}