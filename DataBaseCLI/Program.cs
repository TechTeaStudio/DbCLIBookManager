using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var ui = new ConsoleUI();
        var configHandler = ProgramHelpers.CreateConfigHandler();
        var databaseService = new DatabaseService(configHandler.ReadConfig().DatabaseConnectinString);
        var commandHandler = new CommandHandler(ui, databaseService);

        var menu = new Menu(ui, commandHandler);
        menu.Start();
    }
}
