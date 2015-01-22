namespace LK.Config.DirectAuth
{
    public class DirectAuthConfigs
    {
        /// <summary>
        /// 獲取配置類實例
        /// </summary>
        /// <returns></returns>
        public static DirectAuthConfigInfo GetConfig()
        {
            return DirectAuthConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置類實例
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(DirectAuthConfigInfo DirectAuthConfigInfo)
        {
            var fcfm = new DirectAuthConfigFileManager();
            DirectAuthConfigFileManager.ConfigInfo = DirectAuthConfigInfo;
            return fcfm.SaveConfig();
        }
    }
}