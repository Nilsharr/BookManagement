using System.Collections.Generic;
using System.Threading.Tasks;
using BookManagement.DesktopClient.Models;
using BookManagement.Shared.Models;

namespace BookManagement.DesktopClient.Interfaces;

public interface IBookApiService
{
    Task<Result<PagedResponse<Book>, string>> GetAllBooks(int pageSize, string? pageUrl = null, string? author = null,
        string? genre = null);

    Task<Result<Book, string>> GetBook(int id);
    Task<Result<IList<string>, string>> GetAuthors();
    Task<Result<IList<string>, string>> GetGenres();
    Task<Result<string>> AddBook(Book book);
    Task<Result<Book, string>> UpdateBook(Book book);
    Task<Result<string>> DeleteBook(int id);
}