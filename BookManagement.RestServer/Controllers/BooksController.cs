using BookManagement.Data;
using BookManagement.Data.Entities;
using BookManagement.RestServer.Helpers;
using BookManagement.RestServer.Mapping;
using BookManagement.RestServer.Models;
using BookManagement.Shared.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace BookManagement.RestServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(BookDbContext bookDbContext, IValidator<BookDto> bookValidator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<BookDto>>> GetAll([FromQuery] PaginationFilter paginationFilter,
        string? author = null, string? genre = null)
    {
        var bookQuery = bookDbContext.Books.AsQueryable();
        if (!string.IsNullOrWhiteSpace(author))
        {
            bookQuery = bookQuery.Where(x => x.Author == author);
        }

        if (!string.IsNullOrWhiteSpace(genre))
        {
            bookQuery = bookQuery.Where(x => x.Genre == genre);
        }

        var id = paginationFilter.AfterId ?? paginationFilter.BeforeId ?? 0;
        var pagination = new KeysetPagination<Book>(bookQuery, x => x.Id, paginationFilter.PageSize);
        var data = (await pagination.GetPaginatedData(id, !paginationFilter.BeforeId.HasValue)).ToList();

        if (data.Count == 0)
        {
            return Ok(PaginationHelper.CreateEmptyPagedResponse<BookDto>(paginationFilter, Request.Path.Value!,
                Request.Query));
        }

        var hasNext = await pagination.HasNextPage(data);
        var pageProperties = new PaginationProperties
        {
            HasNextPage = hasNext,
            HasPreviousPage = await pagination.HasPreviousPage(data),
            FirstElementId = data[0].Id,
            LastElementId = data[^1].Id,
            LastRecordId = hasNext ? await pagination.LastRecordIdValue() : data[^1].Id,
            TotalCount = await pagination.TotalCount()
        };

        var response = PaginationHelper.CreatePagedResponse(data.MapToDto(), pageProperties, paginationFilter,
            Request.Path.Value!, Request.Query);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> Get(int id)
    {
        var book = await bookDbContext.Books.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (book is null)
        {
            return NotFound();
        }

        return Ok(book.MapToDto());
    }

    [HttpGet("authors")]
    public async Task<ActionResult<IEnumerable<string>>> GetAuthors()
    {
        var authors = await bookDbContext.Books.Select(x => x.Author).Distinct().ToListAsync();
        return Ok(authors);
    }

    [HttpGet("genres")]
    public async Task<ActionResult<IEnumerable<string>>> GetGenres()
    {
        var genres = await bookDbContext.Books.Select(x => x.Genre).Distinct().ToListAsync();
        return Ok(genres);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> Add(BookDto bookDto)
    {
        var validationResult = await bookValidator.ValidateAsync(bookDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var inserted = bookDbContext.Books.Add(bookDto.MapToEntity()).Entity;
        await bookDbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { inserted.Id }, inserted.MapToDto());
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BookDto>> Update(int id, BookDto bookDto)
    {
        if (bookDto.Id != id)
        {
            return BadRequest();
        }

        if (id == 0 || !await bookDbContext.Books.AnyAsync(x => x.Id == id))
        {
            return await Add(bookDto);
        }

        var validationResult = await bookValidator.ValidateAsync(bookDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var updated = bookDbContext.Books.Update(bookDto.MapToEntity()).Entity;
        await bookDbContext.SaveChangesAsync();
        return Ok(updated.MapToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await bookDbContext.Books.Where(x => x.Id == id).DeleteAsync();
        return NoContent();
    }
}