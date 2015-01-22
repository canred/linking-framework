using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
namespace Schedule
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            if (args.Length == 0)
            {

            }
            else {
                var url = args[0];
                WebRequest myWebRequest = WebRequest.Create(url);
                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                myWebRequest.Timeout = 30000;
                using (WebResponse myWebResponse = myWebRequest.GetResponse()) {
                    using (var reader = new StreamReader(myWebResponse.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                    }
                    myWebResponse.Close();
                }
            }
        }
    }
}
