namespace BookManagement.RestServer.Models;

public class PaginationProperties
{
    public required bool HasNextPage { get; init; }
    public required bool HasPreviousPage { get; init; }
    public required long FirstElementId { get; init; }
    public required long LastElementId { get; init; }
    public required long LastRecordId { get; init; }
    public required long TotalCount { get; init; }
}