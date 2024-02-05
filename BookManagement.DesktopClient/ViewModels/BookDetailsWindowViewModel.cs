using System;
using System.Reactive;
using System.Threading.Tasks;
using BookManagement.DesktopClient.Models;
using ReactiveUI;

namespace BookManagement.DesktopClient.ViewModels;

public class BookDetailsWindowViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Book> ReturnBookCommand { get; }

    public string ConfirmButtonContent { get; set; }

    private Book _book;

    public Book Book

    {
        get => _book;
        set => this.RaiseAndSetIfChanged(ref _book, value);
    }

    private DateTimeOffset _selectedDate;

    public DateTimeOffset SelectedDate

    {
        get => _selectedDate;
        set => this.RaiseAndSetIfChanged(ref _selectedDate, value);
    }

    public BookDetailsWindowViewModel()
    {
        ConfirmButtonContent = "Add";
        _book = new Book();
        _selectedDate = DateTimeOffset.Now;
        ReturnBookCommand = ReactiveCommand.CreateFromTask(ReturnBook);
    }

    public BookDetailsWindowViewModel(Book book)
    {
        ConfirmButtonContent = "Edit";
        _book = book;
        _selectedDate = new DateTimeOffset(book.PublicationDate, TimeOnly.MinValue, TimeSpan.Zero);
        ReturnBookCommand = ReactiveCommand.CreateFromTask(ReturnBook);
    }

    private Task<Book> ReturnBook()
    {
        Book.PublicationDate = DateOnly.FromDateTime(SelectedDate.Date);
        return Task.FromResult(Book);
    }
}