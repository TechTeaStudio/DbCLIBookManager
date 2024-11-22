using DatabaseCLI.Framework;

using TechTeaStudio.Config;

internal static class ProgramHelpers
{

    public static ConfigFileHandler<MyConfig> CreateConfigHandler()
    {
        return new ConfigFileHandler<MyConfig>(
            directoryPath: @"C:\TechTeaStudio\Configurations\DbTextManager",
            fileName: "config",
            fileExtension: "json",
            defaultConfig: new MyConfig
            {
                DatabaseConnectinString = "Host=localhost;Port=5897;Database=LibraryDB;Username=postgres;Password=1",
            },
            new JsonConfigSerializer<MyConfig>());
    }
}