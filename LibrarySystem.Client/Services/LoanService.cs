namespace LibrarySystem.Client.Services;
using System.Net.Http.Json;
using LibrarySystem.Client.Models;

public class LoanService
{
    private readonly HttpClient _http;
    public LoanService(HttpClient http) => _http = http;

    public async Task<List<Loan>> GetLoansAsync() => 
        await _http.GetFromJsonAsync<List<Loan>>("api/loans") ?? new();

    public async Task<List<Loan>> GetReaderLoansAsync(int readerId) => 
        await _http.GetFromJsonAsync<List<Loan>>($"api/loans/reader/{readerId}") ?? new();

    public async Task AddLoanAsync(Loan loan) => 
        await _http.PostAsJsonAsync("api/loans", loan);

    public async Task ReturnBookAsync(int loanId) => 
        await _http.PostAsync($"api/loans/return/{loanId}", null);
}