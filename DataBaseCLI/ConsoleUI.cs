
using DatabaseCLI.Framework;

internal sealed class ConsoleUI : IUserInterface
{
    public void Clear() => Console.Clear();
    public void WriteLine(string message) => Console.WriteLine(message);
    public string ReadLine() => Console.ReadLine();
    public ConsoleKeyInfo ReadKey(bool intercept = false) => Console.ReadKey(intercept);

    public void DrawHeader(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('=', Console.WindowWidth));
        Console.WriteLine(title.PadLeft((Console.WindowWidth + title.Length) / 2));
        Console.WriteLine(new string('=', Console.WindowWidth));
        Console.ResetColor();
    }

    public void DrawMenu(string[] items, int selectedItem)
    {
        WriteLine("Use Arrow keys to navigate, Enter to select, ESC to exit.\n");
        for (int i = 0; i < items.Length; i++)
        {
            if (i == selectedItem)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteLine($"> {items[i]}");
            }
            else
            {
                Console.ResetColor();
                WriteLine($"  {items[i]}");
            }
        }
        Console.ResetColor();
    }
}
