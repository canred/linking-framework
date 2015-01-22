namespace LK.Config
{
    /// <summary>
    /// Discuz!NT 配置管理類接口
    /// </summary>
    public interface IConfigFileManager
    {
        /// <summary>
        /// 加載配置文件
        /// </summary>
        /// <returns></returns>
        IConfigInfo LoadConfig();


        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        bool SaveConfig();
    }
}