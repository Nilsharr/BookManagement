using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.Windows.Input;
using BookManagement.DesktopClient.Interfaces;
using BookManagement.DesktopClient.Models;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace BookManagement.DesktopClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private const string AllAuthorsOption = "All Authors";
    private const string AllGenresOption = "All Genres";

    private readonly IBookApiService _bookApiService = App.Current?.Services?.GetService<IBookApiService>() ??
                                                       throw new NullReferenceException(
                                                           "Missing Api Service instance.");

    public ICommand AuthorSelectionChangedCommand => ReactiveCommand.CreateFromTask(AuthorSelectionChanged);
    public ICommand GenreSelectionChangedCommand => ReactiveCommand.CreateFromTask(GenreSelectionChanged);

    public Interaction<BookDetailsWindowViewModel, Book?> ShowBookDetailsDialog { get; } = new();

    public PageProperties PageProperties { get; }

    private ObservableCollection<Book> _books = [];

    public ObservableCollection<Book> Books
    {
        get => _books;
        set => this.RaiseAndSetIfChanged(ref _books, value);
    }

    private Book? _selectedBook;

    public Book? SelectedBook
    {
        get => _selectedBook;
        set => this.RaiseAndSetIfChanged(ref _selectedBook, value);
    }

    private readonly ObservableAsPropertyHelper<bool> _isBookSelected;
    public bool IsBookSelected => _isBookSelected.Value;

    private ObservableCollection<string> _authors = [];

    public ObservableCollection<string> Authors
    {
        get => _authors;
        set => this.RaiseAndSetIfChanged(ref _authors, value);
    }

    public string? SelectedAuthor { get; set; }

    private ObservableCollection<string> _genres = [];

    public ObservableCollection<string> Genres
    {
        get => _genres;
        set => this.RaiseAndSetIfChanged(ref _genres, value);
    }

    public string? SelectedGenre { get; set; }

    public MainWindowViewModel()
    {
        PageProperties = new PageProperties { PageSize = 10 };
        _isBookSelected = this.WhenAnyValue(x => x.SelectedBook).Select(x => x is not null)
            .ToProperty(this, x => x.IsBookSelected);

        RxApp.MainThreadScheduler.Schedule(LoadStartupData);
    }


    private async void LoadStartupData()
    {
        await FirstPage();
        await GetAuthors();
        await GetGenres();
    }

    public async Task FirstPage()
    {
        await LoadPage();
        PageProperties.CurrentPage = PageProperties.TotalPages == 0 ? 0 : 1;
    }

    public async Task LastPage()
    {
        await LoadPage(PageProperties.LastPageUrl);
        PageProperties.CurrentPage = PageProperties.TotalPages;
    }

    public async Task NextPage()
    {
        if (PageProperties.HasNextPage)
        {
            PageProperties.CurrentPage += 1;
            await LoadPage(PageProperties.NextPageUrl);
        }
    }

    public async Task PreviousPage()
    {
        if (PageProperties.HasPreviousPage)
        {
            PageProperties.CurrentPage -= 1;
            await LoadPage(PageProperties.PreviousPageUrl);
        }
    }

    public async Task AddBook()
    {
        var bookDetails = new BookDetailsWindowViewModel();
        var book = await ShowBookDetailsDialog.Handle(bookDetails);
        if (book is not null)
        {
            var result = await _bookApiService.AddBook(book);
            if (result.IsSuccess)
            {
                await FirstPage();
                await ShowSuccessMessageBox("Book has been successfully added.");
            }
            else
            {
                await ShowErrorMessageBox(result.Error!);
            }
        }
    }

    public async Task EditBook()
    {
        if (SelectedBook is not null)
        {
            var bookDetails = new BookDetailsWindowViewModel(Book.Copy(SelectedBook));
            var book = await ShowBookDetailsDialog.Handle(bookDetails);
            if (book is not null && !book.EqualsValue(SelectedBook))
            {
                var result = await _bookApiService.UpdateBook(book);
                if (result.IsSuccess)
                {
                    Books.Replace(SelectedBook, result.Data!);
                    SelectedBook = null;
                    await ShowSuccessMessageBox("Book has been successfully edited.");
                }
                else
                {
                    await ShowErrorMessageBox(result.Error!);
                }
            }
        }
    }

    public async Task DeleteBook()
    {
        if (SelectedBook is null)
        {
            return;
        }

        var msgResult = await ShowYesNoMessageBox("Delete Book", "Are you sure you want to delete this book?",
            Icon.Warning);
        if (msgResult == ButtonResult.No)
        {
            return;
        }

        var result = await _bookApiService.DeleteBook(SelectedBook.Id);
        if (result.IsSuccess)
        {
            await FirstPage();
            await ShowSuccessMessageBox("Book has been successfully removed.");
        }
        else
        {
            await ShowErrorMessageBox(result.Error!);
        }
    }

    private async Task LoadPage(string? pageUrl = null)
    {
        var author = SelectedAuthor == AllAuthorsOption ? null : SelectedAuthor;
        var genre = SelectedGenre == AllGenresOption ? null : SelectedGenre;
        var result = await _bookApiService.GetAllBooks(PageProperties.PageSize, pageUrl, author, genre);
        if (result.IsSuccess)
        {
            UpdatePageProperties(result.Data!.PageProperties);
            Books.Clear();
            Books.Add(result.Data.Data);
        }
        else
        {
            await ShowErrorMessageBox(result.Error!);
        }
    }

    private void UpdatePageProperties(PageProperties pageProperties)
    {
        PageProperties.TotalPages = pageProperties.TotalPages;
        PageProperties.FirstPageUrl = pageProperties.FirstPageUrl;
        PageProperties.LastPageUrl = pageProperties.LastPageUrl;
        PageProperties.NextPageUrl = pageProperties.NextPageUrl;
        PageProperties.PreviousPageUrl = pageProperties.PreviousPageUrl;
    }

    private async Task GetAuthors()
    {
        Authors = [AllAuthorsOption];
        var result = await _bookApiService.GetAuthors();
        if (result.IsSuccess)
        {
            Authors.Add(result.Data!);
        }
        else
        {
            await ShowErrorMessageBox(result.Error!);
        }
    }

    private async Task GetGenres()
    {
        Genres = [AllGenresOption];
        var result = await _bookApiService.GetGenres();
        if (result.IsSuccess)
        {
            Genres.Add(result.Data!);
        }
        else
        {
            await ShowErrorMessageBox(result.Error!);
        }
    }

    private async Task AuthorSelectionChanged()
    {
        await LoadPage();
        PageProperties.CurrentPage = Books.Any() ? 1 : 0;
    }

    private async Task GenreSelectionChanged()
    {
        await LoadPage();
        PageProperties.CurrentPage = Books.Any() ? 1 : 0;
    }

    private static Task<ButtonResult> ShowYesNoMessageBox(string title, string message, Icon icon = Icon.None)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, message, ButtonEnum.YesNo, icon);

        return box.ShowAsync();
    }

    private static async Task ShowOkMessageBox(string title, string message, Icon icon = Icon.None)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, message, ButtonEnum.Ok, icon);

        await box.ShowAsync();
    }

    private static async Task ShowErrorMessageBox(string message)
    {
        await ShowOkMessageBox("Error", message, Icon.Error);
    }

    private static async Task ShowSuccessMessageBox(string message)
    {
        await ShowOkMessageBox("Success", message, Icon.Success);
    }
}