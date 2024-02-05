using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookManagement.Data;

public class BookContextFactory : IDesignTimeDbContextFactory<BookDbContext>
{
    public BookDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Directory.GetCurrentDirectory() + "/../BookManagement.RestServer/appsettings.json").Build();

        var connString = configuration.GetConnectionString("Postgres");

        var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();
        optionsBuilder.UseNpgsql(connString).UseSnakeCaseNamingConvention();

        return new BookDbContext(optionsBuilder.Options);
    }
}