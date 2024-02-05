using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookManagement.DesktopClient.Interfaces;
using BookManagement.DesktopClient.Mapping;
using BookManagement.DesktopClient.Models;
using BookManagement.Shared.Dto;
using BookManagement.Shared.Models;

namespace BookManagement.DesktopClient.Services;

public class BookApiService : IBookApiService
{
    private const string GenericErrorMessage = "An unexpected error occurred. Please try again later.";
    private static HttpClient _httpClient = null!;
    private const string BaseRoute = "/api/Books";

    public BookApiService(string uri)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(uri) };
    }

    public async Task<Result<PagedResponse<Book>, string>> GetAllBooks(int pageSize, string? pageUrl = null,
        string? author = null, string? genre = null)
    {
        var url = pageUrl ?? $"{BaseRoute}?pageSize={pageSize}" +
            (author is null ? "" : $"&author={author}") +
            (genre is null ? "" : $"&genre={genre}");

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResponseDto<BookDto>>(url);
            return response is null ? GenericErrorMessage : response.MapToModel();
        }
        catch (HttpRequestException ex)
        {
            return ex.Message;
        }
    }

    public async Task<Result<Book, string>> GetBook(int id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<BookDto>($"{BaseRoute}/{id}");
            return response is null ? GenericErrorMessage : response.MapToModel();
        }
        catch (HttpRequestException ex)
        {
            return ex.Message;
        }
    }

    public async Task<Result<IList<string>, string>> GetAuthors()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{BaseRoute}/authors") ??
                           Array.Empty<string>();
            return response.ToList();
        }
        catch (HttpRequestException ex)
        {
            return ex.Message;
        }
    }

    public async Task<Result<IList<string>, string>> GetGenres()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{BaseRoute}/genres") ??
                           Array.Empty<string>();
            return response.ToList();
        }
        catch (HttpRequestException ex)
        {
            return ex.Message;
        }
    }

    public async Task<Result<string>> AddBook(Book book)
    {
        using var response = await _httpClient.PostAsJsonAsync(BaseRoute, book.MapToDto());
        return response.IsSuccessStatusCode ? Result<string>.Success() : response.ReasonPhrase ?? GenericErrorMessage;
    }

    public async Task<Result<Book, string>> UpdateBook(Book book)
    {
        using var response = await _httpClient.PutAsJsonAsync($"{BaseRoute}/{book.Id}", book.MapToDto());
        if (!response.IsSuccessStatusCode)
        {
            return response.ReasonPhrase ?? GenericErrorMessage;
        }

        var updatedBook = await response.Content.ReadFromJsonAsync<BookDto>();
        return updatedBook is null ? GenericErrorMessage : updatedBook.MapToModel();
    }

    public async Task<Result<string>> DeleteBook(int id)
    {
        using var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
        return response.IsSuccessStatusCode ? Result<string>.Success() : response.ReasonPhrase ?? GenericErrorMessage;
    }
}