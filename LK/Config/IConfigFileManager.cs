namespace LK.Config
{
    /// <summary>
    /// Discuz!NT �t�m�޲z�����f
    /// </summary>
    public interface IConfigFileManager
    {
        /// <summary>
        /// �[���t�m���
        /// </summary>
        /// <returns></returns>
        IConfigInfo LoadConfig();


        /// <summary>
        /// �O�s�t�m���
        /// </summary>
        /// <returns></returns>
        bool SaveConfig();
    }
}