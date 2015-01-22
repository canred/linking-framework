using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace LK
{
    public class Cloud : IDisposable
    {
        private string CLOUD_ID = null;
        public string Cloud_Id {
            get {
                return CLOUD_ID;
            }
            set {
                CLOUD_ID = value;
            }
        }

        private string _directUrl = null;
        private string _application = null;
        private string _comapny = null;
        private string _account = null;

        public JToken CallDirect(string directUrl, string methodAction, string[] data, string COLUDID)
        {
            Uri target = new Uri(directUrl);
            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("CLOUD_ID", COLUDID);
            string postData = "";
            postData = "{\"action\":\"" + methodAction.Split('.')[0] + "\",\"method\":\"" + methodAction.Split('.')[1] + "\",\"data\":[";
            if (data != null)
            {
                foreach (var tmp in data)
                {
                    postData += "\"" + tmp + "\",";
                }
                postData = postData.Substring(0, postData.Length - 1);
            }
            
            postData += "],\"type\":\"rpc\",\"tid\":999}";

            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            JObject result = new JObject();
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                System.IO.Stream stream = response.GetResponseStream();
                string json = "";

                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        json += reader.ReadLine();
                    }
                }

                result = JObject.Parse(json);
            }

            return result["result"];
        }

        public string getCloudId(string directUrl,string application,string company,string account , string password)
        {
            LK.Util.Security.SecurityClass sc = new LK.Util.Security.SecurityClass(LK.Util.Security.SecurityClass.CryptoEnum.DES);
            
            var result = CallDirect(directUrl, "UserAction.cloudLogon", new string[] { 
                application, 
                company, 
                account, 
                password 
            }, "");
            
            if (result["validation"].Value<String>() == "OK")
            {
                CLOUD_ID = result["CLOUD_ID"].Value<string>();
                _directUrl = directUrl;
                _application = application;
                _comapny = company;
                _account = account;
                return CLOUD_ID;
            }
            else
            {                
                CLOUD_ID = null;
                return null;
            }
        }

        public bool Logout()
        {

            var result = CallDirect(_directUrl, "UserAction.cloudLogout", new string[] {_application,_comapny,_account  }, this.Cloud_Id);
            if (result["validation"].Value<String>() == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public System.Data.DataTable ConvertToDataTable(string json) {
            System.Data.DataTable dt = null;
            try {
                dt = JsonConvert.DeserializeObject<System.Data.DataTable>(json);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable ConvertToDataTable(JToken jsonObj)
        {
            System.Data.DataTable dt = null;
            try
            {
                dt = JsonConvert.DeserializeObject<System.Data.DataTable>(jsonObj.ToString());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region IDisposable 成員

        void IDisposable.Dispose()
        {
            Logout();
        }

        #endregion
    }
}
