using CommunityToolkit.Mvvm.ComponentModel;

namespace BookManagement.DesktopClient.Models;

public partial class PageProperties : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNextPage))]
    [NotifyPropertyChangedFor(nameof(HasPreviousPage))]
    private int _currentPage;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNextPage))]
    [NotifyPropertyChangedFor(nameof(HasPreviousPage))]
    private int _totalPages;

    public required int PageSize { get; init; } = 30;
    public string FirstPageUrl { get; set; } = null!;
    public string LastPageUrl { get; set; } = null!;

    [ObservableProperty] private string? _nextPageUrl;

    [ObservableProperty] private string? _previousPageUrl;

    public bool HasNextPage => CurrentPage != TotalPages;

    public bool HasPreviousPage => TotalPages != 0 && CurrentPage != 1;
}