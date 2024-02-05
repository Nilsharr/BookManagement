namespace BookManagement.Shared.Dto;

public class PagedResponseDto<T>
{
    public required int PageSize { get; init; }
    public required int TotalPages { get; init; }
    public required long TotalRecords { get; init; }
    public required string FirstPageUrl { get; init; }
    public required string LastPageUrl { get; init; }
    public string? NextPageUrl { get; init; }
    public string? PreviousPageUrl { get; init; }
    public required IEnumerable<T> Data { get; init; }
}