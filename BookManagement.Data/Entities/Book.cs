using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Data.Entities;

public class Book : IEntityTypeConfiguration<Book>
{
    public int Id { get; init; }
    public string Author { get; init; } = default!;
    public string Title { get; init; } = default!;
    public string Genre { get; init; } = default!;
    public string Publisher { get; init; } = default!;
    public string Language { get; init; } = default!;
    public DateOnly PublicationDate { get; init; }

    void IEntityTypeConfiguration<Book>.Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Author).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Genre).HasMaxLength(128);
        builder.Property(x => x.Publisher).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Language).IsRequired().HasMaxLength(128);
        builder.Property(x => x.PublicationDate).IsRequired();
    }
}