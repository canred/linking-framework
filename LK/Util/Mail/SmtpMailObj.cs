using System;
using System.Collections.Generic;
using System.Net.Mail;
using log4net;
using System.Reflection;
namespace LK.Util.Mail
{
    public class SmtpMailObj
    {
        #region private property
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private List<string> _attachmentFilePathList = new List<string>();
        private string _bcc = "";
        private string _cc = "";
        private string _contents = "";
        private string _from = "";
        private bool _isBodyHtml = true;
        private MailPriority _priority = MailPriority.Normal;
        private string _subject = "";
        private string _to = "";

        #endregion

        #region 屬性

        /// <summary>
        /// 寄件人 email address
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// 收件人 email address
        /// </summary>
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        /// <summary>
        /// cc email address
        /// </summary>
        public string CC
        {
            get { return _cc; }
            set { _cc = value; }
        }

        /// <summary>
        /// blind copy 的 email address
        /// </summary>
        public string BCC
        {
            get { return _bcc; }
            set { _bcc = value; }
        }

        /// <summary>
        /// mail 內容格式
        /// </summary>
        public bool IsBodyHtml
        {
            get { return _isBodyHtml; }
            set { _isBodyHtml = value; }
        }

        /// <summary>
        /// mail 優先等級
        /// </summary>
        public MailPriority Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// mail 主旨
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// mail 本文
        /// </summary>
        public string Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        /// <summary>
        /// 是否要 throw exception
        /// </summary>
        public bool ThrowException { get; set; }

        /// <summary>
        /// 附件路徑
        /// </summary>
        public List<string> AttachmentFilePathList
        {
            get { return _attachmentFilePathList; }
            set { _attachmentFilePathList = value; }
        }

        #endregion //end of 屬性

        public List<string> GetToList()
        {
            try
            {
                var toList = new List<string>();
                if (_to.Contains(",") || _to.Contains(";"))
                {
                    foreach (string str in _to.Split(new char[] { ',', ';' }))
                    {
                        toList.Add(str);
                    }
                }
                else {
                    toList.Add(_to);
                }
                //else if (_cc.Contains(";"))
                //{
                //    foreach (string str in _cc.Split(Convert.ToChar(";")))
                //    {
                //        ccLLK.Add(str);
                //    }
                //}

                return toList;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<string> GetCCList()
        {
            try
            {
                var ccList = new List<string>();
                if (_cc.Contains(",") || _cc.Contains(";"))
                {
                    foreach (string str in _cc.Split(new char[] { ',', ';' }))
                    {
                        ccList.Add(str);
                    }
                }
                else {
                    ccList.Add(_cc);
                }
                //else if (_cc.Contains(";"))
                //{
                //    foreach (string str in _cc.Split(Convert.ToChar(";")))
                //    {
                //        ccLLK.Add(str);
                //    }
                //}

                return ccList;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<string> GetBCCList()
        {
            try
            {
                var bccList = new List<string>();
                if (_bcc.Contains(",") || _bcc.Contains(";"))
                {
                    foreach (string str in _bcc.Split(new char[] { ',', ';' }))
                    {
                        bccList.Add(str);
                    }
                }
                else {
                    bccList.Add(_bcc);
                }
                //else if (_bcc.Contains(";"))
                //{
                //    foreach (string str in _bcc.Split(Convert.ToChar(";")))
                //    {
                //        bccLLK.Add(str);
                //    }
                //}

                return bccList;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}