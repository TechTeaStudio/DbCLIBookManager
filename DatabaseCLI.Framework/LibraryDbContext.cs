using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Reflection.Emit;

public class Book
{
    public int id { get; set; }
    public string title { get; set; }
    public string author { get; set; }
    public string genre { get; set; }
    public DateTime published_date { get; set; }
    public int pages { get; set; }
    public string language { get; set; }
}

public class LibraryDbContext : DbContext
{
    public DbSet<Book> books { get; set; }

    private readonly string _connectionString;

    public LibraryDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasIndex(b => b.title);  // Индекс на поле title
        modelBuilder.Entity<Book>()
                 .Property(b => b.published_date)
                 .HasConversion(
                     v => v.ToUniversalTime(), // При записи конвертировать в UTC
                     v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // При чтении устанавливать Kind=Utc
    );
    }
}
