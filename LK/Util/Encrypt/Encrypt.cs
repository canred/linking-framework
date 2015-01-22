using LK.Util.Security;
using log4net;
using System.Reflection;
namespace LK.Util
{
    public static class Encrypt
    {
        static string keyStr = "IT/SOA/ATTR_LIB";
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pwd">傳入值</param>
        /// <returns>回傳加密的字串</returns>
        public static string pwdEncode(string strInput)
        {
            try
            {
                string strKey = keyStr;
                var secObj = new SecurityClass(SecurityClass.CryptoEnum.DES);
                string strSecurty = secObj.Encrypting(strInput, strKey);
                return strSecurty;
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pwd">傳入值</param>
        /// <returns>回傳解密的字串</returns>
        public static string pwdDecode(string strInput)
        {
            try
            {
                string strKey = keyStr;
                var secObj = new SecurityClass(SecurityClass.CryptoEnum.DES);
                string strSecurty = secObj.Decrypting(strInput, strKey);
                return strSecurty;
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}