using BookManagement.Data.Entities;
using BookManagement.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Data;

public sealed class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookDbContext).Assembly);
        //modelBuilder.Seed(58);
    }
}