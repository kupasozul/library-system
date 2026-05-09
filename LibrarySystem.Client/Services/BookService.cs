using LibrarySystem.Client.Models;

namespace LibrarySystem.Client.Services;
using System.Net.Http.Json;

public class BookService
{
    private readonly HttpClient _http;
    public BookService(HttpClient http) => _http = http;

    public async Task<List<Book>> GetBooksAsync() => 
        await _http.GetFromJsonAsync<List<Book>>("api/books") ?? new();

    public async Task<List<Book>> GetAvailableBooksAsync() => 
        await _http.GetFromJsonAsync<List<Book>>("api/books/available") ?? new();

    public async Task AddBookAsync(Book book) => 
        await _http.PostAsJsonAsync("api/books", book);

    public async Task UpdateBookAsync(int id, Book book) => 
        await _http.PutAsJsonAsync($"api/books/{id}", book);

    public async Task DeleteBookAsync(int id) => 
        await _http.DeleteAsync($"api/books/{id}");
}