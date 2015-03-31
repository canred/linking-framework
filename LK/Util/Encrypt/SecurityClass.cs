using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using log4net;
using System.Reflection;
namespace LK.Util.Security
{
    internal class SecurityClass
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
        #region CryptoEnum enum
        public enum CryptoEnum
        {
            DES = 0, //DES演算法
            RC2 = 1, //RC2 演算法
            Rijndael = 2 //Rijndael 演算法
        }
        #endregion
        private readonly SymmetricAlgorithm mobjCryptoService;
        internal SecurityClass(CryptoEnum CryptoType)
        {            
            try
            {
                mobjCryptoService = new DESCryptoServiceProvider();
                switch (CryptoType)
                {
                    case CryptoEnum.DES:
                        mobjCryptoService = new DESCryptoServiceProvider();
                        break;
                    case CryptoEnum.RC2:
                        mobjCryptoService = new RC2CryptoServiceProvider();
                        break;
                    case CryptoEnum.Rijndael:
                        mobjCryptoService = new RijndaelManaged();
                        break;
                    default:
                        throw new Exception("請指定編碼模式!");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        private byte[] GetLegalKey(string Key)
        {
            try
            {
                string sTemp;
                if (mobjCryptoService.LegalKeySizes.Length > 0)
                {
                    int lessSize = 0, moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
                    //Key的大小需用bits計算
                    while (Key.Length * 8 > moreSize)
                    {
                        //當LegalKeySizes的最大值與最小值SkipSize會為零
                        if (mobjCryptoService.LegalKeySizes[0].SkipSize != 0)
                        {
                            lessSize = moreSize;
                            moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
                        }
                        //SkipSize為零時key值就只能取LegalKeySizes的長度
                        else
                        {
                            Key = Key.Substring(0, moreSize / 8);
                            break;
                        }
                        sTemp = Key.PadRight(moreSize / 8, ' ');
                    }
                    sTemp = Key.PadRight(moreSize / 8, ' ');
                }
                else{
                    sTemp = Key;
                }
                //將key值轉成byte array
                return Encoding.ASCII.GetBytes(sTemp);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        internal string Encrypting(string Source, string Key)
        {
            try
            {
                byte[] bytIn = Encoding.ASCII.GetBytes(Source);
                // 建立 MemoryStream 這樣就不用File去紀錄
                var ms = new MemoryStream();
                byte[] bytKey = GetLegalKey(Key);
                // 設定Key值
                mobjCryptoService.Key = bytKey;
                mobjCryptoService.IV = bytKey;
                // 建立Encryptor Provider
                ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
                var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                // 將加密過的內容寫入 MemoryStream
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                // 將0的byte trim掉
                byte[] bytOut = ms.GetBuffer();
                int i = 0;
                for (i = bytOut.Length - 1; i > 0; i--)
                {
                    //當最後一碼不是0,i是位置需加1變為長度
                    if (bytOut[i] != 0)
                    {
                        i += 1;
                        break;
                    }
                }
                // 轉換為Base64這樣才可使用於xml
                return Convert.ToBase64String(bytOut, 0, i);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        internal string Decrypting(string Source, string Key)
        {
            try
            {
                if (!IsBase64String(Source))
                {
                    return Source;
                }
                // 將 Base64 轉換成 binary
                byte[] bytIn = Convert.FromBase64String(Source);
                // 建立 MemoryStream
                var ms = new MemoryStream(bytIn, 0, bytIn.Length);
                byte[] bytKey = GetLegalKey(Key);
                // 設定Key值
                mobjCryptoService.Key = bytKey;
                mobjCryptoService.IV = bytKey;
                // 建立Decryptor Provider
                ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
                var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                // 將結果自Crypto Stream讀出
                try
                {
                    var sr = new StreamReader(cs);
                    return sr.ReadToEnd();
                }
                catch
                {
                    return Source;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        internal bool IsBase64String(string s)
        {
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(s)){
                        return false;
                    }
                    Convert.FromBase64String(s);
                    return true;
                }
                catch
                {
                    return false;
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