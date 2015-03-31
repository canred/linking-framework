namespace LK.Config
{
    public interface IConfigFileManager
    {
        IConfigInfo LoadConfig();
        bool SaveConfig();
    }
}