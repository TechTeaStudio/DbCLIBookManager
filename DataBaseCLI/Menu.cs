
using DatabaseCLI.Framework;

internal sealed class Menu
{
    private readonly IUserInterface _ui;
    private readonly ICommandHandler _commandHandler;
    private readonly string[] _menuItems = { "Add Record", "Delete Record", "Search Records", "Update Record", "Exit" };

    public Menu(IUserInterface ui, ICommandHandler commandHandler)
    {
        _ui = ui;
        _commandHandler = commandHandler;
    }

    public void Start()
    {
        int selectedItem = 0;
        bool running = true;

        while (running)
        {
            _ui.DrawHeader("TechTeaStudio");
            _ui.DrawMenu(_menuItems, selectedItem);

            var key = _ui.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedItem = (selectedItem == 0) ? _menuItems.Length - 1 : selectedItem - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedItem = (selectedItem == _menuItems.Length - 1) ? 0 : selectedItem + 1;
                    break;
                case ConsoleKey.Enter:
                    running = _commandHandler.ExecuteCommand(_menuItems[selectedItem]);
                    break;
                case ConsoleKey.Escape:
                    running = false;
                    break;
            }
        }

        _ui.Clear();
        _ui.WriteLine("Goodbye!");
    }
}
