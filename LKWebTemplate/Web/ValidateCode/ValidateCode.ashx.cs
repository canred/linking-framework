using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Web.SessionState;  

namespace DJSI.handler
{
    public class ValidateCode : IHttpHandler,IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            CreateCheckCodeImage(GenerateCheckCode(context), context);
        }

        private string GenerateCheckCode(HttpContext context)
        {
	        int number;
	        char code;
	        string checkCode = String.Empty;
	        System.Random random = new Random();
	        for (int i = 0; i < 5; i++)
	        {
		        number = random.Next();
		        if (number % 2 == 0)
		            code = (char)('0' + (char)(number % 10));
		        else
		            code = (char)('A' + (char)(number % 26));
		        checkCode += code.ToString();
	        }
	        //儲存在cookie
	         //context.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
	        //儲存在session
            LKWebTemplate.StoreSession ss = new LKWebTemplate.StoreSession();            
            ss.setObject("CheckCode", checkCode);
            //context.Session["CheckCode"] = checkCode;
	        return checkCode;
        }
        private void CreateCheckCodeImage(string checkCode, HttpContext context)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling
            ((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //產生亂數
                Random random = new Random();
                //清空背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                
                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush
                brush = new System.Drawing.Drawing2D.
                LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Red, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //圖片邊框
                g.DrawRectangle(new Pen(Color.Black),
                0, 0, image.Width - 1, image.Height - 1);

                
                


                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                context.Response.ClearContent();
                context.Response.ContentType = "image/Gif";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            { return false; }
        }
    }
}
