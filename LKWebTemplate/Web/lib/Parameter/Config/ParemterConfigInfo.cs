using System;
using LK.Config;

namespace LKWebTemplate.Parameter.Config
{
    [Serializable]
    public class ParemterConfigInfo : IConfigInfo
    {

        private string _appName = "";
        public string AppName
        {
            get { return _appName; }
            set { _appName = value; }
        }

        private string _defaultPage = "";
        public string DefaultPage
        {
            get { return _defaultPage; }
            set { _defaultPage = value; }
        }

        private string _authenticationType = "";
        public string AuthenticationType
        {
            get { return _authenticationType; }
            set { _authenticationType = value; }
        }

        private bool _enableGuestLogin = false;
        public bool EnableGuestLogin
        {
            get { return _enableGuestLogin; }
            set { _enableGuestLogin = value; }
        }

        private bool _WhereAnyChangeAccount = false;
        public bool WhereAnyChangeAccount
        {
            get { return _WhereAnyChangeAccount; }
            set { _WhereAnyChangeAccount = value; }
        }

        private string _guestAccount = "";
        public string GuestAccount
        {
            get { return _guestAccount; }
            set { _guestAccount = value; }
        }

        private string _guestCompany = "";
        public string GuestCompany
        {
            get { return _guestCompany; }
            set { _guestCompany = value; }
        }

        private bool _isProductionServer;
        public bool IsProductionServer
        {
            get { return _isProductionServer; }
            set { _isProductionServer = value; }
        }

        private string _logonPage = "";
        public string LogonPage
        {
            get { return _logonPage; }
            set { _logonPage = value; }
        }

        private string _webRoot = "";
        public string WebRoot
        {
            get { return _webRoot; }
            set { _webRoot = value; }
        }

        private string _devUserCompany = "";
        public string DEVUserCompany
        {
            get { return _devUserCompany; }
            set { _devUserCompany = value; }
        }

        private string _devUserAccount = "";
        public string DEVUserAccount
        {
            get { return _devUserAccount; }
            set { _devUserAccount = value; }
        }

        private string _devUserPassword = "";
        public string DEVUserPassword
        {
            get { return _devUserPassword; }
            set { _devUserPassword = value; }
        }

        private string _noPermissionPage = "";
        public string NoPermissionPage
        {
            get { return _noPermissionPage; }
            set { _noPermissionPage = value; }
        }

        private string _logoutPage = "";
        public string LogoutPage
        {
            get { return _logoutPage; }
            set { _logoutPage = value; }
        }


        private string _UploadFolder = "";
        public string UploadFolder
        {
            get { return _UploadFolder; }
            set { _UploadFolder = value; }
        }

        private string _Title = "";
        /// <summary>
        /// 系統的標題文字
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }


        private string _SystemIcon = "";
        /// <summary>
        /// 系統使用的ICons位置
        /// </summary>
        public string SystemIcon
        {
            get { return _SystemIcon; }
            set { _SystemIcon = value; }
        }

        private string _CompanyImage = "";
        /// <summary>
        /// master page 的公司圖片
        /// </summary>
        public string CompanyImage
        {
            get { return _CompanyImage; }
            set { _CompanyImage = value; }
        }

        private string _SystemName = "";
        /// <summary>
        /// 系統完整名稱
        /// </summary>
        public string SystemName
        {
            get { return _SystemName; }
            set { _SystemName = value; }
        }

        private string _SystemDescription = "";
        /// <summary>
        /// 系統英文名與詳細說明
        /// </summary>
        public string SystemDescription
        {
            get { return _SystemDescription; }
            set { _SystemDescription = value; }
        }

        private string _SystemFoolter = "";
        /// <summary>
        /// master foolter 的文字內容
        /// </summary>
        public string SystemFoolter
        {
            get { return _SystemFoolter; }
            set { _SystemFoolter = value; }
        }
        private string _InitCompany = "";
        public string InitCompany
        {
            get { return _InitCompany; }
            set { _InitCompany = value; }
        }
        private string _InitCompanyUuid = "";
        public string InitCompanyUuid
        {
            get { return _InitCompanyUuid; }
            set { _InitCompanyUuid = value; }
        }
        private string _InitAdmin = "";
        public string InitAdmin
        {
            get { return _InitAdmin; }
            set { _InitAdmin = value; }
        }
        private string _InitAdminUuid = "";
        public string InitAdminUuid
        {
            get { return _InitAdminUuid; }
            set { _InitAdminUuid = value; }
        }
        private string _InitAppUuid = "";
        public string InitAppUuid
        {
            get { return _InitAppUuid; }
            set { _InitAppUuid = value; }
        }

        private string _DirectApplicationName = "";
        public string DirectApplicationName
        {
            get { return _DirectApplicationName; }
            set { _DirectApplicationName = value; }
        }


        private int _DirectTimeOut = 30000;
        public int DirectTimeOut
        {
            get { return _DirectTimeOut; }
            set { _DirectTimeOut = value; }
        }

        private bool _GraphicsCertification = false;
        public bool GraphicsCertification
        {
            get { return _GraphicsCertification; }
            set { _GraphicsCertification = value; }
        }




    }
}