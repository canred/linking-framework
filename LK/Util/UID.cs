using System;
using log4net;
using System.Reflection;
namespace LK.Util
{
    /// <summary>
    /// UniqueID ªººK­n´y­z¡C
    /// </summary>
    public sealed class UID
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        #region singleton

        private static readonly UID _instance = new UID();
        private static int num;

        private UID()
        {
        }

        public static UID Instance
        {
            get { return _instance; }
        }

        #endregion singleton

        public String GetUniqueID()
        {
            try
            {
                var AA = new object();
                lock (AA)
                {
                    num = num % 999999 + 1;
                    return DateTime.Now.ToString("yyMMddHHmmss") + num.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}