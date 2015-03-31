namespace LK.Config.Cloud
{
    public class CloudConfigs
    {
        /// <summary>
        /// ����t�m�����
        /// </summary>
        /// <returns></returns>
        public static CloudConfigInfo GetConfig()
        {
            return CloudConfigFileManager.LoadConfig();
        }
        /// <summary>
        /// �O�s�t�m�����
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