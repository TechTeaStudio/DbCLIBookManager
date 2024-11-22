
using DatabaseCLI.Framework;

internal sealed class CommandHandler : ICommandHandler
{
    private readonly IUserInterface _ui;
    private readonly IDatabaseService _databaseService;

    public CommandHandler(IUserInterface ui, IDatabaseService databaseService)
    {
        _ui = ui;
        _databaseService = databaseService;
    }

    public bool ExecuteCommand(string command)
    {
        _ui.Clear();
        _ui.DrawHeader();
        switch (command)
        {
            case "Add Record":
                _databaseService.AddRecord(_ui);
                break;
            case "Delete Record":
                _databaseService.DeleteRecord(_ui);
                break;
            case "Search Records":
                _databaseService.SearchRecords(_ui);
                break;
            case "Update Record":
                _databaseService.UpdateRecord(_ui);
                break;
            case "Exit":
                return false;
        }
        _ui.WriteLine("Press any key to return to menu...");
        Console.ReadKey();
        return true;
    }
}
