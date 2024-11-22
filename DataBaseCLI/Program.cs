using DatabaseCLI.Framework;

using System.Text;

using TechTeaStudio.Config;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Clear();

        string[] menuItems = { "Add Record", "Delete Record", "Search Records", "Update Record", "Exit" };
        int selectedItem = 0;
        bool running = true;

        while (running)
        {
            DrawHeader();
            DrawMenu(menuItems, selectedItem);
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedItem = (selectedItem == 0) ? menuItems.Length - 1 : selectedItem - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedItem = (selectedItem == menuItems.Length - 1) ? 0 : selectedItem + 1;
                    break;
                case ConsoleKey.Enter:
                    ExecuteCommand(menuItems[selectedItem], ref running);
                    break;
                case ConsoleKey.Escape:
                    running = false;
                    break;
            }
        }

        Console.Clear();
        Console.WriteLine("Goodbye!");
    }

    static void DrawHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('=', Console.WindowWidth));
        Console.WriteLine($"TechTeaStudio".PadLeft((Console.WindowWidth + "TechTeaStudio".Length) / 2));
        Console.WriteLine(new string('=', Console.WindowWidth));
        Console.ResetColor();
    }

    static void DrawMenu(string[] menuItems, int selectedItem)
    {
        Console.WriteLine("Use Arrow keys to navigate, Enter to select, ESC to exit.\n");

        for (int i = 0; i < menuItems.Length; i++)
        {
            if (i == selectedItem)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"> {menuItems[i]}");
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine($"  {menuItems[i]}");
            }
        }

        Console.ResetColor();
    }

    static void ExecuteCommand(string command, ref bool running)
    {
        Console.Clear();
        DrawHeader();

        // Чтение строки подключения из конфигурации
        var configHandler = new ConfigFileHandler<MyConfig>(
            directoryPath: @"C:\2024-04-07", // Path to the directory
            fileName: "config", // File name without extension
            fileExtension: "json", // File extension
            defaultConfig: new MyConfig
            {
                DatabaseConnectinString = "Host=localhost;Port=5897;Database=LibraryDB;Username=postgres;Password=1",
            },
            new JsonConfigSerializer<MyConfig>());
        var connectionString = configHandler.ReadConfig().DatabaseConnectinString;

        // Создание контекста базы данных
        using (var db = new LibraryDbContext(connectionString))
        {
            switch (command)
            {
                case "Add Record":
                    AddRecord(db);
                    break;
                case "Delete Record":
                    DeleteRecord(db);
                    break;
                case "Search Records":
                    SearchRecords(db);
                    break;
                case "Update Record":
                    UpdateRecord(db);
                    break;
                case "Exit":
                    running = false;
                    return;
            }
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey(true);
    }


    static void AddRecord(LibraryDbContext db)
    {
        Console.WriteLine("Enter the title of the Book:");
        var title = Console.ReadLine();
        Console.WriteLine("Enter the author of the Book:");
        var author = Console.ReadLine();
        Console.WriteLine("Enter the genre of the Book:");
        var genre = Console.ReadLine();
        Console.WriteLine("Enter the Published Date (YYYY-MM-DD):");
        var publishedDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter the Number of pages:");
        var pages = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the language of the Book:");
        var language = Console.ReadLine();

        var book = new Book
        {
            title = title,
            author = author,
            genre = genre,
            published_date = publishedDate,
            pages = pages,
            language = language
        };

        db.books.Add(book);
        db.SaveChanges();
        Console.WriteLine($"Book '{title}' added to the database.");
    }
    static void UpdateRecord(LibraryDbContext db)
    {
        Console.WriteLine("Enter the ID of the Book you want to update:");
        var bookId = int.Parse(Console.ReadLine());
        var book = db.books.Find(bookId);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        Console.WriteLine($"Current title: {book.title}. Enter new title (leave blank to keep current):");
        var title = Console.ReadLine();
        if (!string.IsNullOrEmpty(title)) book.title = title;

        Console.WriteLine($"Current author: {book.author}. Enter new author (leave blank to keep current):");
        var author = Console.ReadLine();
        if (!string.IsNullOrEmpty(author)) book.author = author;

        Console.WriteLine($"Current genre: {book.genre}. Enter new genre (leave blank to keep current):");
        var genre = Console.ReadLine();
        if (!string.IsNullOrEmpty(genre)) book.genre = genre;

        Console.WriteLine($"Current Published Date: {book.published_date:yyyy-MM-dd}. Enter new Published Date (leave blank to keep current):");
        var publishedDateStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(publishedDateStr)) book.published_date = DateTime.Parse(publishedDateStr);

        Console.WriteLine($"Current pages: {book.pages}. Enter new pages (leave blank to keep current):");
        var pagesStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(pagesStr)) book.pages = int.Parse(pagesStr);

        Console.WriteLine($"Current language: {book.language}. Enter new language (leave blank to keep current):");
        var language = Console.ReadLine();
        if (!string.IsNullOrEmpty(language)) book.language = language;

        db.SaveChanges();
        Console.WriteLine($"Book '{book.title}' updated.");
    }


    static void SearchRecords(LibraryDbContext db)
    {
        Console.WriteLine("Enter search term (search by title or author):");
        var searchTerm = Console.ReadLine();

        var results = db.books
                        .Where(b => b.title.Contains(searchTerm) || b.author.Contains(searchTerm))
                        .ToList();

        if (results.Any())
        {
            Console.WriteLine($"Found {results.Count} book(s):");
            foreach (var book in results)
            {
                Console.WriteLine($"ID: {book.id}, title: {book.title}, author: {book.author}, genre: {book.genre}, Published: {book.published_date:yyyy-MM-dd}, pages: {book.pages}, language: {book.language}");
            }
        }
        else
        {
            Console.WriteLine("No books found matching the search term.");
        }
    }
    static void DeleteRecord(LibraryDbContext db)
    {
        Console.WriteLine("Enter the ID of the Book you want to delete:");
        var bookId = int.Parse(Console.ReadLine());

        var book = db.books.Find(bookId);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        db.books.Remove(book);
        db.SaveChanges();

        Console.WriteLine($"Book '{book.title}' deleted successfully.");
    }
}
