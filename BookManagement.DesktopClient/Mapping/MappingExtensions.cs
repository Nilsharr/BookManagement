using System.Collections.Generic;
using System.Linq;
using BookManagement.DesktopClient.Models;
using BookManagement.Shared.Dto;

namespace BookManagement.DesktopClient.Mapping;

public static class MappingExtensions
{
    public static BookDto MapToDto(this Book book) => new()
    {
        Id = book.Id,
        Author = book.Author,
        Title = book.Title,
        Genre = book.Genre,
        Publisher = book.Publisher,
        Language = book.Language,
        PublicationDate = book.PublicationDate
    };

    public static IEnumerable<BookDto> MapToDto(this IEnumerable<Book> books) => books.Select(MapToDto);

    public static Book MapToModel(this BookDto bookDto) => new()
    {
        Id = bookDto.Id,
        Author = bookDto.Author,
        Title = bookDto.Title,
        Genre = bookDto.Genre,
        Publisher = bookDto.Publisher,
        Language = bookDto.Language,
        PublicationDate = bookDto.PublicationDate
    };

    public static IEnumerable<Book> MapToModel(this IEnumerable<BookDto> booksDto) => booksDto.Select(MapToModel);

    public static PagedResponse<Book> MapToModel(this PagedResponseDto<BookDto> pagedBooks) => new()
    {
        PageProperties = new PageProperties
        {
            TotalPages = pagedBooks.TotalPages,
            PageSize = pagedBooks.PageSize,
            FirstPageUrl = pagedBooks.FirstPageUrl,
            LastPageUrl = pagedBooks.LastPageUrl,
            NextPageUrl = pagedBooks.NextPageUrl,
            PreviousPageUrl = pagedBooks.PreviousPageUrl
        },
        Data = pagedBooks.Data.MapToModel()
    };
}