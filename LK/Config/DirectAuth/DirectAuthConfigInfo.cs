using System;
using LK.Config;
namespace LK.Config.DirectAuth
{
    [Serializable]
    public class DirectAuthConfigInfo : IConfigInfo
    {
        private bool _allowCrossPost = false;
        public bool AllowCrossPost
        {
            get { return _allowCrossPost; }
            set { _allowCrossPost = value; }
        }
        private string _rule = "";
        public string Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }
        private bool _proxyPermission = false;
        public bool ProxyPermission
        {
            get { return _proxyPermission; }
            set { _proxyPermission = value; }
        }
        private string _NoPermissionAction = "";
        public string NoPermissionAction
        {
            get { return _NoPermissionAction; }
            set { _NoPermissionAction = value; }
        }        
    }
}