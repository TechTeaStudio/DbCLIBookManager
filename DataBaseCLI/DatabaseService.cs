
using DatabaseCLI.Framework;

internal sealed class DatabaseService : IDatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
        InitializeContext();
    }

    public void AddRecord(IUserInterface ui)
    {
        ui.WriteLine("Enter the title of the Book:");
        var title = ui.ReadLine();
        ui.WriteLine("Enter the author of the Book:");
        var author = ui.ReadLine();
        ui.WriteLine("Enter the genre of the Book:");
        var genre = ui.ReadLine();
        ui.WriteLine("Enter the Published Date (YYYY-MM-DD):");
        var publishedDate = DateTime.Parse(ui.ReadLine());
        ui.WriteLine("Enter the Number of pages:");
        var pages = int.Parse(ui.ReadLine());
        ui.WriteLine("Enter the language of the Book:");
        var language = ui.ReadLine();

        var book = new Book
        {
            title = title,
            author = author,
            genre = genre,
            published_date = publishedDate,
            pages = pages,
            language = language
        };

        using (var db = new LibraryDbContext(_connectionString))
        {
            db.books.Add(book);
            db.SaveChanges();
        }

        ui.WriteLine($"Book '{title}' added to the database.");
    }

    public void DeleteRecord(IUserInterface ui)
    {
        ui.WriteLine("Enter the ID of the Book you want to delete:");
        var bookId = int.Parse(ui.ReadLine());

        using (var db = new LibraryDbContext(_connectionString))
        {
            var book = db.books.Find(bookId);

            if (book == null)
            {
                ui.WriteLine("Book not found.");
                return;
            }

            db.books.Remove(book);
            db.SaveChanges();

            ui.WriteLine($"Book '{book.title}' deleted successfully.");
        }
    }

    public void SearchRecords(IUserInterface ui)
    {
        ui.WriteLine("Enter search term (search by title or author):");
        var searchTerm = ui.ReadLine();

        using (var db = new LibraryDbContext(_connectionString))
        {
            var results = db.books
                            .Where(b => b.title.Contains(searchTerm) || b.author.Contains(searchTerm))
                            .ToList();

            if (results.Any())
            {
                ui.WriteLine($"Found {results.Count} book(s):");
                foreach (var book in results)
                {
                    ui.WriteLine($"ID: {book.id}, Title: {book.title}, Author: {book.author}, Genre: {book.genre}, Published: {book.published_date:yyyy-MM-dd}, Pages: {book.pages}, Language: {book.language}");
                }
            }
            else
            {
                ui.WriteLine("No books found matching the search term.");
            }
        }
    }

    public void UpdateRecord(IUserInterface ui)
    {
        ui.WriteLine("Enter the ID of the Book you want to update:");
        var bookId = int.Parse(ui.ReadLine());

        using (var db = new LibraryDbContext(_connectionString))
        {
            var book = db.books.Find(bookId);

            if (book == null)
            {
                ui.WriteLine("Book not found.");
                return;
            }

            ui.WriteLine($"Current title: {book.title}. Enter new title (leave blank to keep current):");
            var title = ui.ReadLine();
            if (!string.IsNullOrEmpty(title)) book.title = title;

            ui.WriteLine($"Current author: {book.author}. Enter new author (leave blank to keep current):");
            var author = ui.ReadLine();
            if (!string.IsNullOrEmpty(author)) book.author = author;

            ui.WriteLine($"Current genre: {book.genre}. Enter new genre (leave blank to keep current):");
            var genre = ui.ReadLine();
            if (!string.IsNullOrEmpty(genre)) book.genre = genre;

            ui.WriteLine($"Current Published Date: {book.published_date:yyyy-MM-dd}. Enter new Published Date (leave blank to keep current):");
            var publishedDateStr = ui.ReadLine();
            if (!string.IsNullOrEmpty(publishedDateStr)) book.published_date = DateTime.Parse(publishedDateStr);

            ui.WriteLine($"Current pages: {book.pages}. Enter new pages (leave blank to keep current):");
            var pagesStr = ui.ReadLine();
            if (!string.IsNullOrEmpty(pagesStr)) book.pages = int.Parse(pagesStr);

            ui.WriteLine($"Current language: {book.language}. Enter new language (leave blank to keep current):");
            var language = ui.ReadLine();
            if (!string.IsNullOrEmpty(language)) book.language = language;

            db.SaveChanges();

            ui.WriteLine("Book updated successfully.");
        }
    }

    private LibraryDbContext InitializeContext()
    {
        var dbContext = new LibraryDbContext(_connectionString);

        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}
