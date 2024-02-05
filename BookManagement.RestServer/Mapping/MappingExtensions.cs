using BookManagement.Data.Entities;
using BookManagement.Shared.Dto;

namespace BookManagement.RestServer.Mapping;

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

    public static Book MapToEntity(this BookDto bookDto) => new()
    {
        Id = bookDto.Id,
        Author = bookDto.Author,
        Title = bookDto.Title,
        Genre = bookDto.Genre,
        Publisher = bookDto.Publisher,
        Language = bookDto.Language,
        PublicationDate = bookDto.PublicationDate
    };

    public static IEnumerable<Book> MapToEntity(this IEnumerable<BookDto> booksDto) => booksDto.Select(MapToEntity);
}