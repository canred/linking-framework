namespace LKWebTemplate.Parameter.Config
{
    public class ParemterConfigs
    {
        /// <summary>
        /// 獲取配置類實例
        /// </summary>
        /// <returns></returns>
        public static ParemterConfigInfo GetConfig()
        {
            return ParemterConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置類實例
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