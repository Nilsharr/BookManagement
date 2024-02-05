using System;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

namespace BookManagement.DesktopClient.Models;

public class Book : ReactiveValidationObject
{
    public int Id { get; init; }
    private string _author = string.Empty;

    public string Author
    {
        get => _author;
        set => this.RaiseAndSetIfChanged(ref _author, value);
    }

    private string _title = string.Empty;

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private string _genre = string.Empty;

    public string Genre
    {
        get => _genre;
        set => this.RaiseAndSetIfChanged(ref _genre, value);
    }

    private string _publisher = string.Empty;

    public string Publisher
    {
        get => _publisher;
        set => this.RaiseAndSetIfChanged(ref _publisher, value);
    }

    private string _language = string.Empty;

    public string Language
    {
        get => _language;
        set => this.RaiseAndSetIfChanged(ref _language, value);
    }

    public DateOnly PublicationDate { get; set; }

    public Book()
    {
        this.ValidationRule(x => x.Author, author => !string.IsNullOrWhiteSpace(author), "Author is required.");
        this.ValidationRule(x => x.Title, title => !string.IsNullOrWhiteSpace(title), "Title is required.");
        this.ValidationRule(x => x.Genre, genre => !string.IsNullOrWhiteSpace(genre), "Genre is required.");
        this.ValidationRule(x => x.Publisher, publisher => !string.IsNullOrWhiteSpace(publisher),
            "Publisher is required.");
        this.ValidationRule(x => x.Language, language => !string.IsNullOrWhiteSpace(language), "Language is required.");
    }

    public static Book Copy(Book book) => new()
    {
        Id = book.Id,
        Author = book.Author,
        Title = book.Title,
        Genre = book.Genre,
        Publisher = book.Publisher,
        Language = book.Language,
        PublicationDate = book.PublicationDate
    };

    public bool EqualsValue(Book? b)
    {
        if (b is null)
        {
            return false;
        }

        if (ReferenceEquals(this, b))
        {
            return true;
        }

        return Id == b.Id && Author == b.Author && Title == b.Title && Genre == b.Genre && Publisher == b.Publisher &&
               Language == b.Language && PublicationDate == b.PublicationDate;
    }
}