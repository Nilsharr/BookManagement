using System.Collections.Generic;

namespace BookManagement.DesktopClient.Models;

public class PagedResponse<T>
{
    public required PageProperties PageProperties { get; init; }

    public required IEnumerable<T> Data { get; init; }
}