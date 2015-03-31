namespace LK.Config.DirectAuth
{
    public class DirectAuthConfigs
    {
        public static DirectAuthConfigInfo GetConfig()
        {
            return DirectAuthConfigFileManager.LoadConfig();
        }
        public static bool SaveConfig(DirectAuthConfigInfo DirectAuthConfigInfo)
        {
            var fcfm = new DirectAuthConfigFileManager();
            DirectAuthConfigFileManager.ConfigInfo = DirectAuthConfigInfo;
            return fcfm.SaveConfig();
        }
    }
}