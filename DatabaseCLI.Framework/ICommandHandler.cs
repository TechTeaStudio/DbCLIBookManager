namespace DatabaseCLI.Framework;

public interface ICommandHandler
{
    bool ExecuteCommand(string command);
}
