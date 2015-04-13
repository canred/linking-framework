using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using System.Reflection;

namespace LKWebTemplate.Util
{

    public static partial class Lan 
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static List<System.Data.DataTable> dtLan = new List<System.Data.DataTable>();

        public static string getLan(string lanFTag)
        {
            try
            {
                string attendantUuid = LK.UserInfo.User.Uuid();
                LanFlag lf = LanFlag.US;
                LKWebTemplate.Model.Basic.BasicModel mod = new Model.Basic.BasicModel();
                var drsAttendant = mod.getAttendant_By_Uuid(attendantUuid).AllRecord();
                if (drsAttendant.Count == 1)
                {
                    var drAttendant = drsAttendant.First();
                    var codepage = drAttendant.CODE_PAGE.ToUpper();
                    if (codepage == "CHT")
                    {
                        lf = LanFlag.CHT;
                    }
                    else if (codepage == "CHS")
                    {
                        lf = LanFlag.CHS;
                    }
                    else if (codepage == "JPN")
                    {
                        lf = LanFlag.JPN;
                    }
                    else
                    {
                        lf = LanFlag.US;
                    }
                }

                return getLan(lf, lanFTag);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.ErrorStaticClass(ex);
                throw ex;
            }

        }
        private static string findTag(System.Data.DataTable dt, string lanFTag) {
            try
            {
                var findDr = dt.Select("LANTAG='" + lanFTag.ToUpper() + "'");
                if (findDr.Count() == 1)
                {
                    return findDr.First()["VALUE"].ToString();
                }
                else if (findDr.Count() > 1)
                {
                    throw new Exception(lanFTag + "有多次定義!");
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.ErrorStaticClass(ex);
                throw ex;
            }
        }
        public static string getLan(LanFlag lf, string lanFTag)
        {
            try
            {
                string ret = "";
                string filename = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                filename += "js/" + Enum.GetName(typeof(LanFlag), lf) + "-Message.js";
                if (!System.IO.File.Exists(filename))
                {
                    throw new Exception("語系檔" + filename + "不存在");
                };
                if (exitCache(lf))
                {
                    var dt = getCache(lf);
                    return findTag(dt, lanFTag);
                }

                System.IO.StreamReader sr = new System.IO.StreamReader(filename);
                string allContent = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                var s = allContent.IndexOf("=");
                var e = allContent.LastIndexOf("}");
                allContent = allContent.Substring(s + 1, e - (s + 1) + 1);
                var jobj = JObject.Parse(allContent);
                System.Data.DataTable _dt = new System.Data.DataTable(Enum.GetName(typeof(LanFlag), lf).ToUpper());
                _dt.Columns.Add("LANTAG");
                _dt.Columns.Add("VALUE");
                _dt.AcceptChanges();
                foreach (var node in jobj)
                {
                    string tmpTag = node.Key;
                    string tmpValue = node.Value.ToString();
                    var newRow = _dt.NewRow();
                    newRow["LANTAG"] = tmpTag;
                    newRow["VALUE"] = tmpValue;
                    _dt.Rows.Add(newRow);
                };
                dtLan.Add(_dt);
                return findTag(getCache(lf), lanFTag);
            }
            catch (Exception ex) {
                log.Error(ex);
                LK.MyException.MyException.ErrorStaticClass(ex);
                throw ex;
            }
        }

        public static bool exitCache(LanFlag lf) {
            try
            {
                bool ret = false;
                foreach (var item in dtLan)
                {
                    if (item.TableName.ToUpper() == Enum.GetName(typeof(LanFlag), lf).ToUpper())
                    {
                        ret = true;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.ErrorStaticClass(ex);
                throw ex;
            }
        }

        public static System.Data.DataTable getCache(LanFlag lf)
        {
            try
            {
                System.Data.DataTable ret = null;
                foreach (var item in dtLan)
                {
                    if (item.TableName.ToUpper() == Enum.GetName(typeof(LanFlag), lf).ToUpper())
                    {
                        ret = item;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.ErrorStaticClass(ex);
                throw ex;
            }
        }

    }

    public enum LanFlag { 
        CHT,
        CHS,
        US,
        JPN
    }  
}