using System.ComponentModel.DataAnnotations;

namespace BookManagement.RestServer.Models;

public class PaginationFilter
{
    public int? AfterId { get; set; }
    public int? BeforeId { get; set; }
    [Range(1, 50)] public int PageSize { get; init; } = 10;
}