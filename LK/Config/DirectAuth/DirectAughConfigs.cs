namespace LK.Config.DirectAuth
{
    public class DirectAuthConfigs
    {
        /// <summary>
        /// ����t�m�����
        /// </summary>
        /// <returns></returns>
        public static DirectAuthConfigInfo GetConfig()
        {
            return DirectAuthConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// �O�s�t�m�����
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