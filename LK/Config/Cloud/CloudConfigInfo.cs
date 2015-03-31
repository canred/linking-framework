using System;
using LK.Config;
namespace LK.Config.Cloud
{
    [Serializable]
    public class CloudConfigInfo : IConfigInfo
    {
        private bool _supportCloud = false;
        public bool SupportCloud
        {
            get { return _supportCloud; }
            set { _supportCloud = value; }
        }
        private bool _isAuthCenter = false;
        public bool IsAuthCenter
        {
            get { return _isAuthCenter; }
            set { _isAuthCenter = value; }
        }
        private string _AuthMaster = "";
        public string AuthMaster 
        {
            get { return _AuthMaster; }
            set { _AuthMaster = value; }
        }
        private string _CloudNode = "";
        public string CloudNode {
            get { return _CloudNode; }
            set { _CloudNode = value; }
        }
        private string _Role = "";
        public string Role
        {
            get { return _Role; }
            set { _Role = value; }
        }
        private string _Slave = "";
        public string Slave
        {
            get { return _Slave; }
            set { _Slave = value; }
        }
        private string _Twins = "";
        public string Twins
        {
            get { return _Twins; }
            set { _Twins = value; }
        }
        private string _CloudKeyPub = "";
        public string CloudKeyPub
        {
            get { return _CloudKeyPub; }
            set { _CloudKeyPub = value; }
        }
        private string _AuthCenterHost = "";
        public string AuthCenterHost
        {
            get { return _AuthCenterHost; }
            set { _AuthCenterHost = value; }
        }
        private string _AuthCenterIP = "";
        public string AuthCenterIP
        {
            get { return _AuthCenterIP; }
            set { _AuthCenterIP = value; }
        }
        private string _AuthCenterWebRoot = "";
        public string AuthCenterWebRoot
        {
            get { return _AuthCenterWebRoot; }
            set { _AuthCenterWebRoot = value; }
        }
        private string _AuthCenterPrototype = "";
        public string AuthCenterPrototype
        {
            get { return _AuthCenterPrototype; }
            set { _AuthCenterPrototype = value; }
        }
        private string _AuthCenterPort = "";
        public string AuthCenterPort
        {
            get { return _AuthCenterPort; }
            set { _AuthCenterPort = value; }
        }
    }
}