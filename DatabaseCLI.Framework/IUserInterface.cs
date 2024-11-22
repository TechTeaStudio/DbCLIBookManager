namespace DatabaseCLI.Framework;

public interface IUserInterface
{
    void Clear();
    void WriteLine(string message);
    string ReadLine();
    ConsoleKeyInfo ReadKey(bool intercept = false);
    void DrawHeader(string title = "TechTeaStudio");
    void DrawMenu(string[] items, int selectedItem);
}
