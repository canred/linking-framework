using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
namespace Schedule
{

    class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void setConsoleWindowVisibility(bool visible, string title)
        {
            // below is Brandon's code           
            //Sometimes System.Windows.Forms.Application.ExecutablePath works for the caption depending on the system you are running under.          
            IntPtr hWnd = FindWindow(null, title);

            if (hWnd != IntPtr.Zero)
            {
                if (!visible)
                    //Hide the window                   
                    ShowWindow(hWnd, 0); // 0 = SW_HIDE               
                else
                    //Show window again                   
                    ShowWindow(hWnd, 1); //1 = SW_SHOWNORMA          
            }
        }

        [STAThread()]
        static void Main(string[] args)
        {            
            Program p = new Program();
            setConsoleWindowVisibility(false, Console.Title);             
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
                        //Console.Write(result);
                    }
                    myWebResponse.Close();
                }
            }
        }
    }
}
