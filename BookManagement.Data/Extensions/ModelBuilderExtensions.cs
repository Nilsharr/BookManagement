using System.Globalization;
using Bogus;
using BookManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Data.Extensions;

public static class ModelBuilderExtensions
{
    private static readonly HashSet<string> BookGenres =
    [
        "Fantasy", "Science Fiction", "Dystopian", "Action & Adventure", "Mystery", "Horror", "Thriller", "Romance",
        "Autobiography", "Biography", "History", "Travel", "True Crime", "Humor", "Technology",
    ];

    public static void Seed(this ModelBuilder modelBuilder, int count)
    {
        modelBuilder.Entity<Book>().HasData(GenerateRandomBooks(count));
    }

    private static List<Book> GenerateRandomBooks(int count)
    {
        var languages = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures)
            .Select(x => x.EnglishName.Split(" ")[0]).Distinct();
        var bookId = 1;
        var bookFaker = new Faker<Book>()
            .StrictMode(true)
            .RuleFor(x => x.Id, _ => bookId++)
            .RuleFor(x => x.Author, f => f.Name.FullName())
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(1, 2).TrimEnd('.'))
            .RuleFor(x => x.Genre, f => f.PickRandom<string>(BookGenres))
            .RuleFor(x => x.Publisher, f => f.Company.CompanyName())
            .RuleFor(x => x.Language, f => f.PickRandom(languages))
            .RuleFor(x => x.PublicationDate, f => DateOnly.FromDateTime(f.Date.Past(10)));

        return bookFaker.Generate(count);
    }
}