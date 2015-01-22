using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace LKWebTemplate.Util.Session
{
    /// <summary>
    /// Page的執行的動作
    /// </summary>
    public enum StoreAction
    {
        Get,
        Set,
        Nothing
    }

    /// <summary>
    /// StoreSession 的摘要描述
    /// </summary>
    public class Store
    {
        private const string StoreName = "COOKIE";

        public string SessionId
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Session.SessionID;
                return "";
            }
        }

        #region Store Start

        #region getStoreAction

        /// <summary>
        /// 依頁面的參數，傳回指定的StoreAction
        /// </summary>
        /// <param name="strAction"></param>
        /// <returns></returns>
        public StoreAction getStoreAction(string strAction)
        {
            StoreAction ret;

            switch (strAction.ToUpper())
            {
                case "GET":
                    ret = StoreAction.Get;
                    break;
                case "SET":
                    ret = StoreAction.Set;
                    break;
                default:
                    ret = StoreAction.Nothing;
                    break;
            }
            return ret;
        }

        #endregion //end of getStoreAction

        #region getValue
        public string getCloudId() {
            return getValue("CLOUD_ID").ToString();
        }
        public string getValue(string key)
        {
            new List<DictionaryEntry>();
            try
            {
                List<DictionaryEntry> store = getCookieInSession();
                string ret;
                if (ExistKey(key))
                {
                    DictionaryEntry tmp = (from item in store
                                           where item.Key.ToString().ToUpper() == key.ToUpper()
                                           select item).First();
                    ret = tmp.Value.ToString();
                }
                else
                {
                    ret = "";
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion //end of getValue

        #region getObject

        public object getObject(string key)
        {
            try
            {
                List<DictionaryEntry> store = getCookieInSession();
                object ret;
                if (ExistKey(key))
                {
                    DictionaryEntry tmp = (from item in store
                                           where item.Key.ToString().ToUpper() == key.ToUpper()
                                           select item).First();
                    ret = tmp.Value;
                }
                else
                {
                    ret = null;
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion //end of getObject

        #region setValue

        public void setValue(string key, string value)
        {
            var store = new List<DictionaryEntry>();
            var tmp = new DictionaryEntry();
            HttpContext context = HttpContext.Current;
            try
            {
                if (context.Session[StoreName] == null)
                {
                    context.Session[StoreName] = store;
                }
                else
                {
                    store = getCookieInSession();
                }
                if (ExistKey(key))
                {
                    tmp = (from item in store
                           where item.Key.ToString().ToUpper() == key.ToUpper()
                           select item).First();

                    store.Remove(tmp);
                    tmp.Value = value;
                    store.Add(tmp);
                }
                else
                {
                    tmp.Key = key;
                    tmp.Value = value;
                    store.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion //end of setValue

        #region setObject

        public void setObject(string key, object value)
        {
            var store = new List<DictionaryEntry>();
            var tmp = new DictionaryEntry();
            HttpContext context = HttpContext.Current;
            try
            {
                if (context.Session[StoreName] == null)
                {
                    context.Session[StoreName] = store;
                }
                else
                {
                    store = getCookieInSession();
                }
                if (ExistKey(key))
                {
                    tmp = (from item in store
                           where item.Key.ToString().ToUpper() == key.ToUpper()
                           select item).First();

                    store.Remove(tmp);
                    tmp.Value = value;
                    store.Add(tmp);
                }
                else
                {
                    tmp.Key = key;
                    tmp.Value = value;
                    store.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion // end of setObject

        #region getCookieInSession

        public List<DictionaryEntry> getCookieInSession()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session[StoreName] as List<DictionaryEntry> == null)
            {
                var store = new List<DictionaryEntry>();
                var tmp = new DictionaryEntry {Key = "Default", Value = ""};
                store.Add(tmp);
                context.Session[StoreName] = store;
            }
            return context.Session[StoreName] as List<DictionaryEntry>;
        }

        #endregion // end of getCookieInSession

        #region ExistKey

        public bool ExistKey(string key)
        {
            List<DictionaryEntry> store = getCookieInSession();
            bool ret = (from tmp in store
                        where tmp.Key.ToString().ToUpper() == key.ToUpper()
                        select tmp).Count() > 0
                           ? true
                           : false;
            return ret;
        }

        #endregion //end of ExistKey

        #region ClearCookieInSession

        public void ClearCookieInSession()
        {
            HttpContext context = HttpContext.Current;
            context.Session.Clear();
        }

        #endregion

        #region ClearCookieInSession

        public void ClearCookieInSession(string key)
        {
            var store = new List<DictionaryEntry>();
            new DictionaryEntry();
            HttpContext context = HttpContext.Current;

            if (context.Session[StoreName] != null)
            {
                store = getCookieInSession();
            }
            if (ExistKey(key))
            {
                DictionaryEntry tmp = (from item in store
                                       where item.Key.ToString().ToUpper() == key.ToUpper()
                                       select item).First();

                store.Remove(tmp);
            }
        }

        #endregion

        #endregion // end of Store Start
    }
}
