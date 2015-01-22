namespace LKWebTemplate.Parameter.Config
{
    public class ParemterConfigs
    {
        /// <summary>
        /// ����t�m�����
        /// </summary>
        /// <returns></returns>
        public static ParemterConfigInfo GetConfig()
        {
            return ParemterConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// �O�s�t�m�����
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(ParemterConfigInfo ParemterConfigInfo)
        {
            var fcfm = new ParemterConfigFileManager();
            ParemterConfigFileManager.ConfigInfo = ParemterConfigInfo;
            return fcfm.SaveConfig();
        }
    }
}