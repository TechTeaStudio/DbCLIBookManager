namespace DatabaseCLI.Framework;

public interface IDatabaseService
{
    void AddRecord(IUserInterface ui);
    void DeleteRecord(IUserInterface ui);
    void SearchRecords(IUserInterface ui);
    void UpdateRecord(IUserInterface ui);
}
