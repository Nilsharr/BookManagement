namespace BookManagement.Shared.Dto;

public class BookDto
{
    public int Id { get; init; }
    public required string Author { get; init; }
    public required string Title { get; init; }
    public required string Genre { get; init; }
    public required string Publisher { get; init; }
    public required string Language { get; init; }
    public required DateOnly PublicationDate { get; init; }
}