namespace LK.Config.Cloud
{
    public class CloudConfigs
    {
        /// <summary>
        /// 獲取配置類實例
        /// </summary>
        /// <returns></returns>
        public static CloudConfigInfo GetConfig()
        {
            return CloudConfigFileManager.LoadConfig();
        }
        /// <summary>
        /// 保存配置類實例
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(CloudConfigInfo CloudConfigInfo)
        {
            var fcfm = new CloudConfigFileManager();
            CloudConfigFileManager.ConfigInfo = CloudConfigInfo;
            return fcfm.SaveConfig();
        }
    }
}