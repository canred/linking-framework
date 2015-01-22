using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LK.UserInfo
{
    public interface IUserInfoConfigInfo
    {
        System.Xml.XmlDocument GetBaseConfig();
        System.Data.DataTable GetBaseConfig_DataTable();
        string GetTag(string actionTag, bool alwaysReLoad);
        string GetTag(string ActionTag);      
    }
}
