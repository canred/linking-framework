using System;
using System.Net.Mail;
using System.Text;

using log4net;
using System.Reflection;

namespace LK.Util.Mail
{
    public class SmtpMailer
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        #region Send
        public static void Send(SmtpMailObj mailObj)
        {
            try
            {
                _Send(mailObj);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion //end of Send

        private static void _Send(SmtpMailObj mailObj)
        {
            SMTPConfigInfo SMTPConfigInfo = new SMTPConfigInfo();
            try
            {
                if (string.IsNullOrEmpty(mailObj.From.Trim()))
                {
                    
                    if (!string.IsNullOrEmpty(SMTPConfigInfo.FromEmail.Trim()))
                    {
                        mailObj.From = SMTPConfigInfo.FromEmail;
                    }
                    else
                        throw new Exception("沒有設定寄件人 email address!");
                }

                if (string.IsNullOrEmpty(mailObj.To.Trim()))
                    throw new Exception("沒有設定收件人 email address!");

                if (string.IsNullOrEmpty(mailObj.Subject.Trim()))
                    throw new Exception("沒有設定 email 主旨!");

                if (string.IsNullOrEmpty(mailObj.Contents.Trim()))
                    throw new Exception("沒有設定 email 內容!");

                MailMessage mail;

                if (SMTPConfigInfo.IsSendMail.Trim() == "Y")
                {
                    //MaillMessage物件....
                    //mail = new MailMessage(mailObj.From.Trim(), mailObj.To.Trim(), mailObj.Subject.Trim(),
                    //                       mailObj.Contents);



                    mail = new MailMessage();

                    mail.From = new MailAddress(mailObj.From.Trim());
                    mail.Subject = mailObj.Subject.Trim();
                    mail.Body = mailObj.Contents;


                    foreach (string addr in mailObj.GetToList())
                    {
                        if (!string.IsNullOrEmpty(addr.Trim()))
                            mail.To.Add(new MailAddress(addr.Trim()));
                    }

                    foreach (string addr in mailObj.GetCCList())
                    {
                        if (!string.IsNullOrEmpty(addr.Trim()))
                            mail.CC.Add(new MailAddress(addr.Trim()));
                    }

                    foreach (string addr in mailObj.GetBCCList())
                    {
                        if (!string.IsNullOrEmpty(addr.Trim()))
                            mail.Bcc.Add(new MailAddress(addr.Trim()));
                    }

                    if (SMTPConfigInfo.IsSendAdminMail.Trim().ToUpper() == "Y")
                    {
                        if (!string.IsNullOrEmpty(SMTPConfigInfo.AdministratorEmail.Trim()))
                        {
                            foreach (string addr in SMTPConfigInfo.AdministratorEmail.Split(new char[] { ',', ';' }))
                            {
                                mail.Bcc.Add(new MailAddress(addr.Trim()));
                            }
                        }
                    }

                    if (SMTPConfigInfo.IsSendDebugMail.Trim().ToUpper() == "Y")
                    {
                        if (!string.IsNullOrEmpty(SMTPConfigInfo.DebugEmail.Trim()))
                        {
                            foreach (string addr in SMTPConfigInfo.DebugEmail.Split(new char[] { ',', ';' }))
                            {
                                mail.Bcc.Add(new MailAddress(addr.Trim()));
                            }
                        }
                    }

                    foreach (string path in mailObj.AttachmentFilePathList)
                    {
                        if (string.IsNullOrEmpty(path.Trim()))
                        {
                            var att = new Attachment(path);
                            mail.Attachments.Add(att);
                        }
                    }
                }
                else
                {
                    var sbContent = new StringBuilder();
                    sbContent.Append(mailObj.Contents.Trim());

                    //sbContent.Append("<br><br>Actual Mail:<br>To:" + mailObj.To.Trim());

                    sbContent.Append("<br><br>Actual Mail:<br>To:");
                    foreach (string addr in mailObj.GetToList())
                    {
                        sbContent.Append(addr.Trim() + ",");
                    }

                    sbContent.Append("<br>cc:");
                    foreach (string addr in mailObj.GetCCList())
                    {
                        sbContent.Append(addr.Trim()+",");
                    }

                    sbContent.Append("<br>bcc:");
                    foreach (string addr in mailObj.GetBCCList())
                    {
                        sbContent.Append(addr.Trim()+",");
                    }


                    mail = new MailMessage();
                    mail.From = new MailAddress(mailObj.From.Trim());
                    mail.Subject = mailObj.Subject.Trim();
                    mail.Body = sbContent.ToString();
                    foreach(var addr in SMTPConfigInfo.DebugEmail.Split(new char[]{';',','})){
                        if(addr.Trim().Length>0){
                            mail.To.Add(addr.Trim());
                        }
                    }
                    //mail = new MailMessage(mailObj.From.Trim(), SMTPConfigInfo.DebugEmail, mailObj.Subject.Trim(),
                                           //sbContent.ToString());
                }
                mail.IsBodyHtml = mailObj.IsBodyHtml;
                mail.Priority = mailObj.Priority;


                var mailClinet = new SmtpClient();
                mailClinet.Host = SMTPConfigInfo.SMTPServerHost;
                if (!string.IsNullOrEmpty(SMTPConfigInfo.SMTPServerPort))
                {
                    mailClinet.Port = Convert.ToInt16(SMTPConfigInfo.SMTPServerPort);
                }

                if (SMTPConfigInfo.CredentialsAccount.Trim().Length > 0 && SMTPConfigInfo.CredentialsPassword.Trim().Length > 0) {
                    mailClinet.Credentials = new System.Net.NetworkCredential(SMTPConfigInfo.CredentialsAccount, SMTPConfigInfo.CredentialsPassword);
                };               

                if (SMTPConfigInfo.IsSend.Trim() == "Y")
                {
                    mailClinet.Send(mail);                    
                }

                try
                {
                    log.Info("----Mail Log----");
                    log.Info("System Send DateTime:"+DateTime.Now.ToString("yyyyMMddHHmmss"));
                    log.Info("Title:"+mail.Subject);
                    log.Info("To:" + mail.To.ToString());
                    log.Info("Cc:" + mail.CC.ToString());
                    log.Info("Bcc:" + mail.Bcc.ToString());
                    log.Info("----Content----");
                    log.Info(mail.Body);
                }
                catch (Exception innerEx) { 
                    
                }
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder("It is fail when send mail " + Environment.NewLine);
                sb.Append(" ; From:" + mailObj.From + Environment.NewLine);
                sb.Append(" ; To:" + mailObj.To + Environment.NewLine);
                sb.Append(" ; Subject:" + mailObj.Subject + Environment.NewLine);
                var newException = new Exception(ex + " ; " + sb);
                //if (mailObj.ThrowException)
                //{
                    LK.MyException.MyException.ErrorNoThrowExceptionForStaticClass(ex);
                   // throw MailExceptionPublisher.Publisher(ex, user);
                //}
                //else
                //{
                    //MailExceptionPublisher.Publisher(ex, user);
                //}
            }

            try
            {
                //InsertDB(mailObj.From, mailObj.To, mailObj.Subject, mailObj.Contents);
                //InsertDB(mailObj.From, mailObj.To, mailObj.Subject, "");
            }
            catch (Exception ex)
            {
                var sb =
                    new StringBuilder(
                        "It is successful to send mail but it is fail when insert data into mail_history  " +
                        Environment.NewLine);
                sb.Append(" ; From:" + mailObj.From + Environment.NewLine);
                sb.Append(" ; To:" + mailObj.To + Environment.NewLine);
                sb.Append(" ; Subject:" + mailObj.Subject + Environment.NewLine);
                var newException = new Exception(sb + " ; " + ex);
            }
        }
    }
}