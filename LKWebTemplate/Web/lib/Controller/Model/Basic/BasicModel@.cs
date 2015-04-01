using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LK.Attribute;
using LK.DB;
using LK.DB.SQLCreater;
using LKWebTemplate.Model.Basic.Table;
using LKWebTemplate.Model.Basic.Table.Record;
using System.Security;
using System.DirectoryServices.AccountManagement;
namespace LKWebTemplate.Model.Basic
{
    public partial class BasicModel
    {
      

        public IList<Department_Record> getDepartment_By_CompanyUuid(string pCompanyUuid,OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                Department department = new Department(dbc);
                var ret = department.Where(new SQLCondition(department).Equal(department.COMPANY_UUID, pCompanyUuid))
                    .Limit(orderlimit)
                    .FetchAll<Department_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public System.Data.DataTable getDepartment_By_RootUuid_DataTable(string pRootUuid, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Department appmenu = new LKWebTemplate.Model.Basic.Table.Department(dbc);
                var result = appmenu.Where(new SQLCondition(appmenu)
                    .Equal(appmenu.PARENT_DEPARTMENT_UUID, pRootUuid)
                ).Limit(orderlimit).FetchAll();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Department_Record> getDepartment_By_CompanyUuid(string pCompanyUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                Department department = new Department(dbc);
                var ret = department.Where(new SQLCondition(department).Equal(department.COMPANY_UUID, pCompanyUuid))
                    .FetchAll<Department_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Company_Record> getCompany()
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                Company company = new Company(dbc);
                var ret = company.Where(new SQLCondition(company))
                    .FetchAll<Company_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public IList<Department_Record> getDepartment()
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                Department company = new Department(dbc);
                var ret = company.Where(new SQLCondition(company))
                    .FetchAll<Department_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Attendant_Record> getAttendant()
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                Attendant company = new Attendant(dbc);
                var ret = company.Where(new SQLCondition(company))
                    .FetchAll<Attendant_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }


        #region Attendant
        public IList<Attendant_Record> getAttendant_By_IsActive(string pIsActive, OrderLimit orderLimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Attendant attendant = new LKWebTemplate.Model.Basic.Table.Attendant(dbc);
                var ret = attendant.Where(new SQLCondition(attendant).Equal(attendant.IS_ACTIVE, pIsActive))
                    .Limit(orderLimit)
                    .FetchAll<Attendant_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public Attendant_Record getAttendant_By_CompanyUuid_Account(string pCompanyUuid, string pAccount)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Attendant attendant = new LKWebTemplate.Model.Basic.Table.Attendant(dbc);
                var result = attendant.Where(new SQLCondition(attendant)
                .Equal(attendant.COMPANY_UUID, pCompanyUuid)
                .And()
                .Equal(attendant.ACCOUNT, pAccount))
                .FetchAll<Attendant_Record>();
                if (result.Count == 1)
                {
                    return result.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Attendant_Record> getAttendant_By_CompanyUuid(string pCompanyUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Attendant attendant = new LKWebTemplate.Model.Basic.Table.Attendant(dbc);
                var result = attendant.Where(new SQLCondition(attendant)
                .Equal(attendant.COMPANY_UUID, pCompanyUuid)
                )
                .FetchAll<Attendant_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }


        public IList<AttendantV_Record> getAttendantV_By_Company_Account_Password(string pCompanyName, string pAccount, string pPassword)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                AttendantV attendantv = new AttendantV(dbc);
                var result = attendantv.Where(new SQLCondition(attendantv)
                                                .Equal(attendantv.COMPANY_ID, pCompanyName, false)
                                                .And()
                                                .Equal(attendantv.ACCOUNT, pAccount, false)
                                                .And()
                                                .Equal(attendantv.PASSWORD, pPassword)
                                                ).FetchAll<AttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<AttendantV_Record> getAttendantV_By_Company_Account_Password(string pCompanyName, string pAccount)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                var attendantv = new AttendantV(dbc);
                var result = attendantv.Where(new SQLCondition(attendantv)
                                                .Equal(attendantv.COMPANY_ID, pCompanyName)
                                                .And()
                                                .Equal(attendantv.ACCOUNT, pAccount)
                                                ).FetchAll<AttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<AttendantV_Record> getAttendantV_By_CompanyUuid_KeyWord(string pCompanyUuid, string pKeyword, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AttendantV attendantv = new LKWebTemplate.Model.Basic.Table.AttendantV(dbc);
                var condition = new SQLCondition(attendantv);
                condition.Equal(attendantv.COMPANY_UUID, pCompanyUuid);
                if (pKeyword.Trim().Length > 0)
                {
                    condition.And().L()
                       .iBLike(attendantv.ACCOUNT, pKeyword)
                       .Or()
                       .iBLike(attendantv.C_NAME, pKeyword)
                       .Or()
                       .iBLike(attendantv.DEPARTMENT_C_NAME, pKeyword)
                       .Or()
                       .iBLike(attendantv.DEPARTMENT_E_NAME, pKeyword)
                       .Or()
                       .iBLike(attendantv.DEPARTMENT_ID, pKeyword)
                       .Or()
                       .iBLike(attendantv.E_NAME, pKeyword)
                       .Or()
                       .iBLike(attendantv.EMAIL, pKeyword)
                       .Or()
                       .iBLike(attendantv.PHONE, pKeyword)
                   .R();
                }

                var result = attendantv.Where(condition)
                .Limit(orderlimit)
                .FetchAll<AttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public Int32 getAttendantV_By_CompanyUuid_KeyWord_Count(string pCompanyUuid, string pKeyword)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AttendantV attendantv = new LKWebTemplate.Model.Basic.Table.AttendantV(dbc);
                var result = attendantv.Where(new SQLCondition(attendantv)
                .Equal(attendantv.COMPANY_UUID, pCompanyUuid)
                .And()
                .L()
                    .iBLike(attendantv.ACCOUNT, pKeyword)
                    .Or()
                    .iBLike(attendantv.C_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_C_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_E_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_ID, pKeyword)
                    .Or()
                    .iBLike(attendantv.E_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.EMAIL, pKeyword)
                    .Or()
                    .iBLike(attendantv.PHONE, pKeyword)
                .R()
                ).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<AttendantV_Record> getAttendantV_By_CompanyUuid_KeyWord(string pCompanyUuid,
           string pKeyword, string pIsActive, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AttendantV attendantv = new LKWebTemplate.Model.Basic.Table.AttendantV(dbc);

                SQLCondition sc = new SQLCondition(attendantv);
                sc.Equal(attendantv.COMPANY_UUID, pCompanyUuid)
                .And()
                .L()
                    .iBLike(attendantv.ACCOUNT, pKeyword)
                    .Or()
                    .iBLike(attendantv.C_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_C_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_E_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_ID, pKeyword)
                    .Or()
                    .iBLike(attendantv.E_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.EMAIL, pKeyword)
                    .Or()
                    .iBLike(attendantv.PHONE, pKeyword)
                .R();

                if (pIsActive.Trim().Length > 0)
                {
                    sc.And()
                        .Equal(attendantv.IS_ACTIVE, pIsActive);
                }

                var result = attendantv.Where(sc)
                .Limit(orderlimit)
                .FetchAll<AttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public Int32 getAttendantV_By_CompanyUuid_KeyWord_Count(string pCompanyUuid,
            string pIsActive, string pKeyword)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AttendantV attendantv = new LKWebTemplate.Model.Basic.Table.AttendantV(dbc);
                SQLCondition sc = new SQLCondition(attendantv);

                sc.Equal(attendantv.COMPANY_UUID, pCompanyUuid)
                .And()
                .L()
                    .iBLike(attendantv.ACCOUNT, pKeyword)
                    .Or()
                    .iBLike(attendantv.C_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_C_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_E_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.DEPARTMENT_ID, pKeyword)
                    .Or()
                    .iBLike(attendantv.E_NAME, pKeyword)
                    .Or()
                    .iBLike(attendantv.EMAIL, pKeyword)
                    .Or()
                    .iBLike(attendantv.PHONE, pKeyword)
                .R();

                if (pIsActive.Trim().Length > 0)
                {
                    sc.Equal(attendantv.IS_ACTIVE, pIsActive);
                }

                var result = attendantv.Where(sc).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region Company
        public Int32 getCompany_By_KeyWord_IsActive_Count(string pKeyword, string pIsActive)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Company company = new LKWebTemplate.Model.Basic.Table.Company(dbc);
                var result = company.Where(new SQLCondition(company)
                .L()
                    .iBLike(company.C_NAME, pKeyword)
                    .Or()
                    .iBLike(company.E_NAME, pKeyword)
                    .Or()
                    .iBLike(company.ID, pKeyword)
                    .Or()
                    .iBLike(company.NAME_ZH_CN, pKeyword)

                .R()
                .And()
                .Equal(company.IS_ACTIVE, pIsActive))
                .FetchCount();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Company_Record> getCompany_By_KeyWord_IsActive(string pKeyword, string pIsActive, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Company company = new LKWebTemplate.Model.Basic.Table.Company(dbc);
                var result = company.Where(new SQLCondition(company)
                .L()
                    .iBLike(company.C_NAME, pKeyword)
                    .Or()
                    .iBLike(company.E_NAME, pKeyword)
                    .Or()
                    .iBLike(company.ID, pKeyword)
                    .Or()
                    .iBLike(company.NAME_ZH_CN, pKeyword)

                .R()
                .And()
                .Equal(company.IS_ACTIVE, pIsActive))
                .Limit(orderlimit)
                .FetchAll<Company_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        #endregion

        #region Department
        public System.Data.DataTable getDepartment_By_ParentUuid(string pParentUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Department department = new LKWebTemplate.Model.Basic.Table.Department(dbc);
                var result = department.Where(new SQLCondition(department)
                    .Equal(department.PARENT_DEPARTMENT_UUID, pParentUuid))
                    .Order(new OrderLimit(department.C_NAME, OrderLimit.OrderMethod.ASC))
                    .FetchAll();
                //.FetchAll<LKWebTemplate.Model.Basic.Table.Record.Department_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Department_Record> getDepartment_Root_By_CompanyUuid(string pComapnyUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                Department department = new Department(dbc);

                var result = department.Where(new SQLCondition(department)
                .IsNull(department.PARENT_DEPARTMENT_UUID)
                .And()
                .Equal(department.COMPANY_UUID, pComapnyUuid)
                ).FetchAll<Department_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        #endregion

        #region ApplicationHead
        public int getApplicationHead_By_KeyWord_Count(string pKeyWord)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.ApplicationHead applicationhead = new LKWebTemplate.Model.Basic.Table.ApplicationHead(dbc);
                var result = applicationhead.Where(new SQLCondition(applicationhead)
                .iBLike(applicationhead.DESCRIPTION, pKeyWord)
                .Or()
                .iBLike(applicationhead.ID, pKeyWord)
                .Or()
                .iBLike(applicationhead.NAME, pKeyWord)
                ).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<ApplicationHead_Record> getApplicationHead_By_KeyWord(string pKeyWord, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.ApplicationHead applicationhead = new LKWebTemplate.Model.Basic.Table.ApplicationHead(dbc);
                var result = applicationhead.Where(new SQLCondition(applicationhead)
                .iBLike(applicationhead.DESCRIPTION, pKeyWord)
                .Or()
                .iBLike(applicationhead.ID, pKeyWord)
                .Or()
                .iBLike(applicationhead.NAME, pKeyWord)
                )
                .Limit(orderlimit)
                .FetchAll<ApplicationHead_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public int getSitmapV_By_ApplicationHeadUuid_Count(string pApplicationHeadUuid, string pIsActive)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.SitemapV sitemapv = new LKWebTemplate.Model.Basic.Table.SitemapV(dbc);
                var result = sitemapv.Where(new SQLCondition(sitemapv)
                .Equal(sitemapv.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .Equal(sitemapv.IS_ACTIVE, pIsActive)
                ).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        #endregion

        #region AppPage
        public int getAppPage_By_KeyWord_Count(string pApplicationHeadUuid, string pKeyWord)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Apppage apppage = new LKWebTemplate.Model.Basic.Table.Apppage(dbc);
                var result = apppage.Where(new SQLCondition(apppage)
                .Equal(apppage.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .L()
                    .iBLike(apppage.DESCRIPTION, pKeyWord)
                    .Or()
                    .iBLike(apppage.ID, pKeyWord)
                    .Or()
                    .iBLike(apppage.NAME, pKeyWord)
                    .Or()
                    .iBLike(apppage.URL, pKeyWord)
                .R()
                ).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Apppage_Record> getAppPage_By_KeyWord(string pApplicationHeadUuid, string pKeyWord, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Apppage apppage = new LKWebTemplate.Model.Basic.Table.Apppage(dbc);
                var result = apppage.Where(new SQLCondition(apppage)
                .Equal(apppage.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .L()
                    .iBLike(apppage.DESCRIPTION, pKeyWord)
                    .Or()
                    .iBLike(apppage.ID, pKeyWord)
                    .Or()
                    .iBLike(apppage.NAME, pKeyWord)
                    .Or()
                    .iBLike(apppage.URL, pKeyWord)
                .R()
                )
                .Limit(orderlimit)
                .FetchAll<Apppage_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region Sitemap
        public System.Data.DataTable getSitemapV_By_RootUuid_DataTable(string pRootUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.SitemapV sitemapv = new LKWebTemplate.Model.Basic.Table.SitemapV(dbc);
                var result = sitemapv.Where(new SQLCondition(sitemapv)
                .Equal(sitemapv.ROOT_UUID, pRootUuid))
                .Limit(new OrderLimit(sitemapv.NAME, OrderLimit.OrderMethod.ASC)
                ).FetchAll();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<SitemapV_Record> getSitemapV_By_ApplicationHead(string pApplicationHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.SitemapV sitemapv = new LKWebTemplate.Model.Basic.Table.SitemapV(dbc);
                var result = sitemapv.Where(new SQLCondition(sitemapv)
                .Equal(sitemapv.APPLICATION_HEAD_UUID, pApplicationHeadUuid)

                ).FetchAll<SitemapV_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<SitemapV_Record> getSitmapV_By_ApplicationHeadUuid(string pApplicationHeadUuid, string pIsActive, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.SitemapV sitemapv = new LKWebTemplate.Model.Basic.Table.SitemapV(dbc);
                var result = sitemapv.Where(new SQLCondition(sitemapv)
                .Equal(sitemapv.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .Equal(sitemapv.IS_ACTIVE, pIsActive)
                )
                .Limit(orderlimit)
                .FetchAll<SitemapV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region GroupHead
        public Int32 getGroupHead_By_Count(string company_uuid,
            string application_head_uuid, string attendant_uuid, string searchkey)
        {
            try
            {
                List<string> gh_lst = new List<string>();
                if (!string.IsNullOrEmpty(attendant_uuid))
                {
                    IList<GroupAttendantV_Record> gaV = this.getGroupAttendantBy(application_head_uuid, company_uuid, attendant_uuid);
                    foreach (GroupAttendantV_Record vr in gaV)
                        gh_lst.Add(vr.GROUP_HEAD_UUID);
                }

                if (!string.IsNullOrEmpty(attendant_uuid) && gh_lst.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.getGroupHead_ByParam_Count(company_uuid, application_head_uuid, gh_lst, searchkey);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public Int32 getGroupHead_ByParam_Count(string company_uuid, string application_head_uuid, List<string> group_head_list, string searchkey)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupHeadV table = new LKWebTemplate.Model.Basic.Table.GroupHeadV(dbc);

                SQLCondition con = new SQLCondition(table)
                    .L()
                        .iBLike(table.NAME_ZH_TW, searchkey)
                        .Or()
                        .iBLike(table.NAME_ZH_CN, searchkey)
                        .Or()
                        .iBLike(table.NAME_EN_US, searchkey)
                    .R()
                    .And()
                    .Equal(table.COMPANY_UUID, company_uuid);

                if (application_head_uuid.Trim().Length > 0)
                {
                    con.And()
                    .Equal(table.APPLICATION_HEAD_UUID, application_head_uuid);
                }


                if (group_head_list.Count > 0)
                {
                    int count = 0;
                    con.And()
                        .L();
                    foreach (string s in group_head_list)
                    {
                        if (count == count - 1)
                        {
                            con.Equal(table.UUID, s);
                        }
                        else
                            con.Equal(table.UUID, s).Or();
                        count++;
                    }
                    con.CheckSQL();
                    con.R();
                }

                var result = table.Where(con).FetchCount();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupHeadV_Record> getGroupHead_By(string company_uuid, string application_head_uuid,
            string attendant_uuid, string searchkey, OrderLimit orderlimit)
        {
            try
            {
                List<string> gh_lst = new List<string>();
                if (!string.IsNullOrEmpty(attendant_uuid))
                {
                    IList<GroupAttendantV_Record> gaV = this.getGroupAttendantBy(application_head_uuid, company_uuid, attendant_uuid);
                    foreach (GroupAttendantV_Record vr in gaV)
                        gh_lst.Add(vr.GROUP_HEAD_UUID);
                }

                if (!string.IsNullOrEmpty(attendant_uuid) && gh_lst.Count == 0)
                {
                    return new List<GroupHeadV_Record>();
                }
                else
                {
                    return this.getGroupHead_ByParam
                        (company_uuid, application_head_uuid, gh_lst, searchkey, orderlimit);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupHeadV_Record> getGroupHead_ByParam(
            string company_uuid, string application_head_uuid, List<string> group_head_list, string searchkey,
            OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupHeadV table = new LKWebTemplate.Model.Basic.Table.GroupHeadV(dbc);

                SQLCondition con = new SQLCondition(table)
                    .L()
                        .iBLike(table.NAME_ZH_TW, searchkey)
                        .Or()
                        .iBLike(table.NAME_ZH_CN, searchkey)
                        .Or()
                        .iBLike(table.NAME_EN_US, searchkey)
                    .R()
                    .And()
                    .Equal(table.COMPANY_UUID, company_uuid);

                if (application_head_uuid.Trim().Length > 0)
                {
                    con.And()
                    .Equal(table.APPLICATION_HEAD_UUID, application_head_uuid);
                }

                if (group_head_list.Count > 0)
                {
                    int count = 0;
                    con.And()
                        .L();
                    foreach (string s in group_head_list)
                    {
                        if (count == count - 1)
                        {
                            con.Equal(table.UUID, s);
                        }
                        else
                            con.Equal(table.UUID, s).Or();
                        count++;
                    }
                    con.CheckSQL();
                    con.R();
                }

                var result = table.Where(con)
                    .Order(orderlimit)
                    .FetchAll<GroupHeadV_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAttendantV_Record> getGroupAttendantBy(string application_head_uuid, string company_uuid, string attendant_uuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAttendantV table = new LKWebTemplate.Model.Basic.Table.GroupAttendantV(dbc);

                SQLCondition con = new SQLCondition(table);
                if (application_head_uuid.Trim().Length > 0)
                {
                    con.Equal(table.APPLICATION_HEAD_UUID, application_head_uuid).And();
                }

                if (company_uuid.Trim().Length > 0)
                {
                    con.Equal(table.COMPANY_UUID, company_uuid).And();
                }

                if (attendant_uuid.Trim().Length > 0)
                {
                    con.Equal(table.ATTENDANT_UUID, attendant_uuid).And();
                }

                con.CheckSQL();

                var result = table.Where(con).FetchAll<GroupAttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAttendantV_Record> getGroupAttendantVByGroupHeadUuid(string group_head_uuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAttendantV table = new LKWebTemplate.Model.Basic.Table.GroupAttendantV(dbc);

                SQLCondition con = new SQLCondition(table)
                    .Equal(table.GROUP_HEAD_UUID, group_head_uuid);

                var result = table.Where(con).FetchAll<GroupAttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAttendant_Record> getGroupAttendantByGroupHeadUuid(string group_head_uuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAttendant table = new LKWebTemplate.Model.Basic.Table.GroupAttendant(dbc);

                SQLCondition con = new SQLCondition(table)
                    .Equal(table.GROUP_HEAD_UUID, group_head_uuid);

                var result = table.Where(con).FetchAll<GroupAttendant_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAttendantV_Record> getGroupAttendantVByGroupHeadUuidxAttendantUuid
            (string group_head_uuid, string attendant_uuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAttendantV table = new LKWebTemplate.Model.Basic.Table.GroupAttendantV(dbc);

                SQLCondition con = new SQLCondition(table)
                    .Equal(table.GROUP_HEAD_UUID, group_head_uuid)
                    .And()
                    .Equal(table.ATTENDANT_UUID, attendant_uuid);

                var result = table.Where(con).FetchAll<GroupAttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAttendant_Record> getGroupAttendantByGroupHeadUuidxAttendantUuid
            (string group_head_uuid, string attendant_uuid)
        {
            dbc = LK.Config.DataBase.Factory.getInfo();
            LKWebTemplate.Model.Basic.Table.GroupAttendant table = new LKWebTemplate.Model.Basic.Table.GroupAttendant(dbc);

            SQLCondition con = new SQLCondition(table)
                .Equal(table.GROUP_HEAD_UUID, group_head_uuid)
                .And()
                .Equal(table.ATTENDANT_UUID, attendant_uuid);

            var result = table.Where(con).FetchAll<GroupAttendant_Record>();
            return result;
        }

        public void deleteGroupHeadAll(string group_head_uuid)
        {
            try
            {
                var drGroupAppmenu = getGroupAppmenuV_By_GroupHeadUuid(group_head_uuid);
                if (drGroupAppmenu.Count > 0)
                {
                    foreach (GroupAppmenu_Record r in drGroupAppmenu)
                    {
                        r.gotoTable().Delete(r);
                    }

                }
                var drGroupAttendant = getGroupAttendantByGroupHeadUuid(group_head_uuid);
                if (drGroupAttendant.Count > 0)
                {
                    foreach (GroupAttendant_Record r in drGroupAttendant)
                    {
                        r.gotoTable().Delete(r);
                    }
                }
                var drGroupHead = getGroupHead_By_Uuid(group_head_uuid);
                if (drGroupHead.AllRecord().Count == 1)
                {
                    var data = drGroupHead.AllRecord().First();
                    data.gotoTable().Delete(data);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }


        #endregion GroupHead

        #region Appmenu

        #region getAppmenuV_By_ParentUuid_DataTable
        public System.Data.DataTable getAppmenuApppageV_By_ParentUuid_DataTable(string pParentUuid,OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                AppmenuApppageV appmenuv =
                    new AppmenuApppageV(dbc);
                SQLCondition con = new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPMENU_UUID, pParentUuid);
                
                var result = appmenuv.Where(con)
                     .Limit(orderlimit)
                     .FetchAll();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public IList<AppmenuApppageV_Record> getAppmenuApppageV_By_ParentUuid(string pParentUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                AppmenuApppageV appmenuv =
                    new AppmenuApppageV(dbc);
                SQLCondition con = new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPMENU_UUID, pParentUuid);
                OrderLimit order = new OrderLimit(appmenuv.NAME_ZH_TW, OrderLimit.OrderMethod.ASC);
                var result = appmenuv.Where(con)
                     .Limit(order)
                     .FetchAll<AppmenuApppageV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<AppmenuApppageV_Record> getAppmenuApppageV_By_ApplicationHeadUuid(string pApplicationHeadUuid,OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                AppmenuApppageV appmenuv =
                    new AppmenuApppageV(dbc);
                SQLCondition con = new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPLICATION_HEAD_UUID, pApplicationHeadUuid);
                //OrderLimit order = new OrderLimit(appmenuv.NAME_ZH_TW, OrderLimit.OrderMethod.ASC);
                var result = appmenuv.Where(con)
                     .Limit(orderlimit)
                     .FetchAll<AppmenuApppageV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        
        public IList<Appmenu_Record> getAppmenu_By_ParentUuid(string pParentUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                Appmenu appmenuv =
                    new Appmenu(dbc);
                SQLCondition con = new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPMENU_UUID, pParentUuid);
                OrderLimit order = new OrderLimit(appmenuv.NAME_ZH_TW, OrderLimit.OrderMethod.ASC);
                var result = appmenuv.Where(con)
                     .Limit(order)
                     .FetchAll<Appmenu_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<AppmenuApppageV_Record> getAppmenuV_Root_By_ApplicationHead(string pApplicationHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                AppmenuApppageV appmenuv = new AppmenuApppageV(dbc);
                var result = appmenuv.Where(new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                    .And()
                    .IsNull(appmenuv.APPMENU_UUID)
                ).FetchAll<AppmenuApppageV_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        //public System.Data.DataTable getAppmenuV_By_ParentUuid_DataTable(string pApplicationHeadUuid)
        //{
        //    try
        //    {
        //        dbc = LK.Config.DataBase.Factory.getInfo();
        //        AppmenuApppageV appmenuv =
        //            new AppmenuApppageV(dbc);
        //        SQLCondition con = new SQLCondition(appmenuv)
        //            .Equal(appmenuv.APPLICATION_HEAD_UUID, pApplicationHeadUuid);
        //        OrderLimit order = new OrderLimit(appmenuv.NAME_ZH_TW, OrderLimit.OrderMethod.ASC);
        //        var result = appmenuv.Where(con)
        //             .Limit(order)
        //             .FetchAll();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //        LK.MyException.MyException.Error(this, ex);
        //        throw ex;
        //    }
        //}
        #endregion

        #region getAppmenu_By_ParentUuid_DataTable
        public System.Data.DataTable getAppmenu_By_ParentUuid_DataTable(string pParentUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Appmenu appmenuv =
                    new LKWebTemplate.Model.Basic.Table.Appmenu(dbc);
                SQLCondition con = new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPMENU_UUID, pParentUuid);
                OrderLimit order = new OrderLimit(appmenuv.NAME_ZH_TW, OrderLimit.OrderMethod.ASC);
                var result = appmenuv.Where(con)
                     .Limit(order)
                     .FetchAll();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region getAppmenuV_Root_By_ApplicationHead
        public IList<Appmenu_Record> getAppmenu_Root_By_ApplicationHead(string pApplicationHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AppmenuApppageV appmenuv = new LKWebTemplate.Model.Basic.Table.AppmenuApppageV(dbc);
                var result = appmenuv.Where(new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                    .And()
                    .IsNull(appmenuv.APPMENU_UUID)
                ).FetchAll<Appmenu_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Appmenu_Record> getAppmenu_By_ApplicationHead(string pApplicationHeadUuid,OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AppmenuApppageV appmenuv = new LKWebTemplate.Model.Basic.Table.AppmenuApppageV(dbc);
                var result = appmenuv.Where(new SQLCondition(appmenuv)
                    .Equal(appmenuv.APPLICATION_HEAD_UUID, pApplicationHeadUuid)                    
                ).Limit(orderlimit).FetchAll<Appmenu_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region getAppmenu_By_RootUuid_DataTable
        public System.Data.DataTable getAppmenu_By_RootUuid_DataTable(string pRootUuid,OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Appmenu appmenu = new LKWebTemplate.Model.Basic.Table.Appmenu(dbc);
                var result = appmenu.Where(new SQLCondition(appmenu)
                    .Equal(appmenu.APPMENU_UUID, pRootUuid)
                ).Limit(orderlimit).FetchAll();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region getAppMenu_By_ApplicationHead
        public IList<Appmenu_Record> getAppMenu_By_ApplicationHead(string pApplicationHeadUuid, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Appmenu appmenu = new LKWebTemplate.Model.Basic.Table.Appmenu(dbc);
                var result = appmenu.Where(new SQLCondition(appmenu)
                .Equal(appmenu.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                )
                .Limit(orderlimit)
                .FetchAll<Appmenu_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion getAppMenu_By_ApplicationHead

        #region getAppMenu_By_ApplicationHead_Count
        public int getAppMenu_By_ApplicationHead_Count(string pApplicationHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Appmenu appmenu = new LKWebTemplate.Model.Basic.Table.Appmenu(dbc);
                var result = appmenu.Where(new SQLCondition(appmenu)
                .Equal(appmenu.APPLICATION_HEAD_UUID, pApplicationHeadUuid)

                ).FetchCount();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        public int getAppMenu_By_ApplicationHeadAndSitemap_Count(string pApplicationHeadUuid,
           string pSiteMapUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Appmenu appmenu = new LKWebTemplate.Model.Basic.Table.Appmenu(dbc);
                var result = appmenu.Where(new SQLCondition(appmenu)
                .Equal(appmenu.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .Equal(appmenu.SITEMAP_UUID, pSiteMapUuid)

                ).FetchCount();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public int getAppMenu_MaxOrd(string pApplicationHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Appmenu appmenu = new LKWebTemplate.Model.Basic.Table.Appmenu(dbc);

                var result = appmenu.Where(new SQLCondition(appmenu)
                    .Equal(appmenu.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                ).Select("MAX(ord) maxord").FetchAll();
                if (result.Rows.Count == 1)
                {
                    return System.Convert.ToInt32(result.Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion Appmenu

        #region GroupAppmenu

        #region getGroupAppmenuV_By_GroupHeadUuid
        public IList<GroupAppmenu_Record> getGroupAppmenuV_By_GroupHeadUuid(string pGroupHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAppmenuV groupAppMenuv = new LKWebTemplate.Model.Basic.Table.GroupAppmenuV(dbc);
                var result = groupAppMenuv.Where(new SQLCondition(groupAppMenuv)
                    .Equal(groupAppMenuv.GROUP_HEAD_UUID, pGroupHeadUuid)
                ).FetchAll<GroupAppmenu_Record>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region getGroupAppmenuV_By_Param
        public IList<GroupAppmenu_Record> getGroupAppmenuV_By_Param
            (string pAppmenuUuid, string pGroupHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAppmenu groupAppMenuv = new LKWebTemplate.Model.Basic.Table.GroupAppmenu(dbc);
                var result = groupAppMenuv.Where(new SQLCondition(groupAppMenuv)
                    .Equal(groupAppMenuv.GROUP_HEAD_UUID, pGroupHeadUuid)
                    .And()
                    .Equal(groupAppMenuv.APPMENU_UUID, pAppmenuUuid)
                ).FetchAll<GroupAppmenu_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion getGroupAppmenuV_By_Param

        #region getGroupAppmenuV_By_AppMenuUuid
        public IList<GroupAppmenu_Record> getGroupAppmenuV_By_AppMenuUuid
            (string pGroupHeadUuid, IList<string> AppMenuUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAppmenu groupAppMenuv = new LKWebTemplate.Model.Basic.Table.GroupAppmenu(dbc);
                int count = 0;
                SQLCondition sc = new SQLCondition(groupAppMenuv)
                    .Equal(groupAppMenuv.GROUP_HEAD_UUID, pGroupHeadUuid);
                if (AppMenuUuid.Count > 0)
                {
                    sc.And()
                        .L();
                    foreach (string s in AppMenuUuid)
                    {
                        if (count < AppMenuUuid.Count - 1)
                            sc.Equal(groupAppMenuv.APPMENU_UUID, s)
                                .Or();
                        else
                            sc.Equal(groupAppMenuv.APPMENU_UUID, s);

                        count++;
                    }
                    sc.R();
                }

                var result = groupAppMenuv.Where(sc).FetchAll<GroupAppmenu_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion getGroupAppmenuV_By_Param

        #region getAttendantNotInGroupAttendant
        /// <summary>
        /// /// 取得公司且不包於群組中人員清單
        /// </summary>
        /// <param name="company_uuid"></param>
        /// <param name="group_head_uuid"></param>
        /// <param name="search_text"></param>
        /// <param name="orderLimit"></param>
        /// <returns></returns>
        public IList<AttendantV_Record> getAttendantNotInGroupAttendant(string company_uuid,
            string group_head_uuid, string search_text, string pIsActive, OrderLimit orderLimit)
        {
            try
            {
                IList<AttendantV_Record> attLst = getAttendantV_By_CompanyUuid_KeyWord(company_uuid, search_text, pIsActive, orderLimit);
                IList<GroupAttendantV_Record> groupLst = getGroupAttendantVByGroupHeadUuid(group_head_uuid);

                return (from c in attLst where !(from s in groupLst select s.ATTENDANT_UUID).Contains(c.UUID) select c).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region getAttendantInGroupAttendant
        /// <summary>
        /// /// 取得公司且包於群組中人員清單
        /// </summary>
        /// <param name="company_uuid"></param>
        /// <param name="group_head_uuid"></param>
        /// <param name="search_text"></param>
        /// <param name="orderLimit"></param>
        /// <returns></returns>
        public IList<AttendantV_Record> getAttendantInGroupAttendant(string company_uuid,
            string group_head_uuid, string search_text, string pIsActive, OrderLimit orderLimit)
        {
            try
            {
                IList<AttendantV_Record> attLst = getAttendantV_By_CompanyUuid_KeyWord(company_uuid, search_text, pIsActive, orderLimit);
                IList<GroupAttendantV_Record> groupLst = getGroupAttendantVByGroupHeadUuid(group_head_uuid);

                return (from c in attLst where (from s in groupLst select s.ATTENDANT_UUID).Contains(c.UUID) select c).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        #endregion

        #endregion

        #region GroupAttendant

        public Int32 getGroupHead_By_Count(string pGroupHeadUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAttendantV table = new LKWebTemplate.Model.Basic.Table.GroupAttendantV(dbc);

                SQLCondition con = new SQLCondition(table)

                    .Equal(table.GROUP_HEAD_UUID, pGroupHeadUuid);

                var result = table.Where(con).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAttendantV_Record> getGroupHead_By(string pGroupHeadUuid, OrderLimit orderLimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAttendantV table = new LKWebTemplate.Model.Basic.Table.GroupAttendantV(dbc);

                SQLCondition con = new SQLCondition(table)

                    .Equal(table.GROUP_HEAD_UUID, pGroupHeadUuid);

                var result = table.Where(con)
                                            .Limit(orderLimit)
                                            .FetchAll<GroupAttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        #endregion

        #region authority_menu_v
        public IList<AuthorityMenuV_Record> getAuthorityMenuVByAttendantUuid(string attendant_uuid, string application_head_uuid)
        {
            dbc = LK.Config.DataBase.Factory.getInfo();
            LKWebTemplate.Model.Basic.Table.AuthorityMenuV table = new LKWebTemplate.Model.Basic.Table.AuthorityMenuV(dbc);


            SQLCondition con = new SQLCondition(table)

                .Equal(table.ATTENDANT_UUID, attendant_uuid)
                .And()
                .Equal(table.APPLICATION_HEAD_UUID, application_head_uuid);

            var result = table.Where(con).FetchAll<AuthorityMenuV_Record>();
            return result;
        }

        #endregion

        #region ErrorLog

        #region getErrorLog_ByIsRead
        public IList<ErrorLogV_Record> getErrorLog_ByIsRead(string is_read, OrderLimit orderLimit)
        {
            dbc = LK.Config.DataBase.Factory.getInfo();
            LKWebTemplate.Model.Basic.Table.ErrorLogV table = new LKWebTemplate.Model.Basic.Table.ErrorLogV(dbc);
            SQLCondition con = new SQLCondition(table)
                .Equal(table.IS_READ, is_read);

            var result = table
                                .Where(con)
                                .Limit(orderLimit)
                                .FetchAll<ErrorLogV_Record>();
            return result;
        }
        #endregion

        #region getErrorLog_ByIsRead_Count
        public int getErrorLog_ByIsRead_Count(string is_read)
        {
            dbc = LK.Config.DataBase.Factory.getInfo();
            LKWebTemplate.Model.Basic.Table.ErrorLogV table = new LKWebTemplate.Model.Basic.Table.ErrorLogV(dbc);
            SQLCondition con = new SQLCondition(table)
                .Equal(table.IS_READ, is_read);

            var result = table.Where(con).FetchCount();
            return result;
        }
        #endregion

        #region getAllErrorLog_ByIsRead
        public IList<ErrorLog_Record> getAllErrorLog_ByIsRead(string is_read)
        {
            dbc = LK.Config.DataBase.Factory.getInfo();
            LKWebTemplate.Model.Basic.Table.ErrorLog table = new LKWebTemplate.Model.Basic.Table.ErrorLog(dbc);
            SQLCondition con = new SQLCondition(table)
                .Equal(table.IS_READ, is_read);

            var result = table
                                .Where(con)
                                .FetchAll<ErrorLog_Record>();
            return result;
        }
        #endregion

        #endregion

        #region Proxy
        public int getProxy_By_KeyWord_Count(string pApplicationHeadUuid, string pKeyWord)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Proxy proxy = new LKWebTemplate.Model.Basic.Table.Proxy(dbc);
                var result = proxy.Where(new SQLCondition(proxy)
                .Equal(proxy.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .L()
                    .iBLike(proxy.DESCRIPTION, pKeyWord)
                    .Or()
                    .iBLike(proxy.PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(proxy.PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(proxy.REDIRECT_PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(proxy.REDIRECT_PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(proxy.PROXY_TYPE, pKeyWord)
                .R()
                ).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Proxy_Record> getProxy_By_KeyWord(string pApplicationHeadUuid, string pKeyWord, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Proxy proxy = new LKWebTemplate.Model.Basic.Table.Proxy(dbc);
                var result = proxy.Where(new SQLCondition(proxy)
                .Equal(proxy.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .L()
                    .iBLike(proxy.DESCRIPTION, pKeyWord)
                    .Or()
                    .iBLike(proxy.PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(proxy.PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(proxy.REDIRECT_PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(proxy.REDIRECT_PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(proxy.PROXY_TYPE, pKeyWord)
                .R()
                )
                .Limit(orderlimit).FetchAll<Proxy_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //LK.MyException.MyException.Error(this, ex);
                LK.MyException.MyException.ErrorNoThrowException(this, ex);
                throw ex;
            }
        }

        public IList<AppmenuProxyMap_Record> getAppmenuProxyMap_By_AppMenuUuid(string pAppMenuUuid, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AppmenuProxyMap appmenuproxymap = new LKWebTemplate.Model.Basic.Table.AppmenuProxyMap(dbc);
                return appmenuproxymap.Where(new SQLCondition(appmenuproxymap)
                .Equal(appmenuproxymap.APPMENU_UUID, pAppMenuUuid)
                ).Limit(orderlimit).
                FetchAll<AppmenuProxyMap_Record>();
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion

        #region  VAppmenuProxyMap
        public int getVAppmenuProxyMap_Count(string pApplicationHeadUuid, string pAppmenuUuid, string pKeyWord)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.VAppmenuProxyMap table = new LKWebTemplate.Model.Basic.Table.VAppmenuProxyMap(dbc);
                var condition = new SQLCondition(table)
                .Equal(table.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .L()
                    .iBLike(table.NAME_EN_US, pKeyWord)
                    .Or()
                    .iBLike(table.NAME_ZH_CN, pKeyWord)
                    .Or()
                    .iBLike(table.NAME_ZH_TW, pKeyWord)
                    .Or()
                    .iBLike(table.PROXY_DESCRIPTION, pKeyWord)
                    .Or()
                    .iBLike(table.PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(table.PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(table.REDIRECT_PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(table.REDIRECT_PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(table.REDIRECT_SRC, pKeyWord)
                .R();

                if (pAppmenuUuid.Trim().Length > 0)
                {
                    condition.And()
                        .Equal(table.APPMENU_UUID, pAppmenuUuid);
                }

                var result = table.Where(condition).FetchCount();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<VAppmenuProxyMap_Record> getVAppmenuProxyMap(string pApplicationHeadUuid, string pAppmenuUuid, string pKeyWord, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.VAppmenuProxyMap table = new LKWebTemplate.Model.Basic.Table.VAppmenuProxyMap(dbc);
                var condition = new SQLCondition(table)
                .Equal(table.APPLICATION_HEAD_UUID, pApplicationHeadUuid)
                .And()
                .L()
                    .iBLike(table.NAME_EN_US, pKeyWord)
                    .Or()
                    .iBLike(table.NAME_ZH_CN, pKeyWord)
                    .Or()
                    .iBLike(table.NAME_ZH_TW, pKeyWord)
                    .Or()
                    .iBLike(table.PROXY_DESCRIPTION, pKeyWord)
                    .Or()
                    .iBLike(table.PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(table.PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(table.REDIRECT_PROXY_ACTION, pKeyWord)
                    .Or()
                    .iBLike(table.REDIRECT_PROXY_METHOD, pKeyWord)
                    .Or()
                    .iBLike(table.REDIRECT_SRC, pKeyWord)
                .R();

                if (pAppmenuUuid.Trim().Length > 0)
                {
                    condition.And()
                        .Equal(table.APPMENU_UUID, pAppmenuUuid);
                }

                var result = table.Where(condition).Limit(orderlimit).FetchAll<VAppmenuProxyMap_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        #endregion


        public IList<AppmenuProxyMap_Record> getAppmenuProxyMap_By_AppMenuUuid_ProxyUuid(string pAppMenuUuid, string pProxyUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.AppmenuProxyMap appmenuproxymap = new LKWebTemplate.Model.Basic.Table.AppmenuProxyMap(dbc);
                return appmenuproxymap.Where(new SQLCondition(appmenuproxymap)
                .Equal(appmenuproxymap.APPMENU_UUID, pAppMenuUuid)
                .And()
                .Equal(appmenuproxymap.PROXY_UUID, pProxyUuid)
                ).FetchAll<AppmenuProxyMap_Record>();
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<VAuthProxy_Record> getVAuthProxy_By_AttendantUuid(string pAttendantUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.VAuthProxy vauthproxy = new LKWebTemplate.Model.Basic.Table.VAuthProxy(dbc);
                return vauthproxy.Where(new SQLCondition(vauthproxy)
                .Equal(vauthproxy.ATTENDANT_UUID, pAttendantUuid)
                ).FetchAll<VAuthProxy_Record>();

            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public int getVAuthProxy_By_AttendantUuid_ProxyAction_ProxyMethod(string pAttendantUuid, string pProxyAction, string pProxyMethod)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.VAuthProxy vauthproxy = new LKWebTemplate.Model.Basic.Table.VAuthProxy(dbc);
                return vauthproxy.Where(new SQLCondition(vauthproxy)
                .Equal(vauthproxy.ATTENDANT_UUID, pAttendantUuid)
                .And()
                .Equal(vauthproxy.PROXY_ACTION, pProxyAction)
                .And()
                .Equal(vauthproxy.PROXY_METHOD, pProxyMethod)
                ).FetchCount();

            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public int getVScheduleTime_By_Keyword_Count(string pKeyWork)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.VScheduleTime vscheduletime = new LKWebTemplate.Model.Basic.Table.VScheduleTime(dbc);
                return vscheduletime.Where(new SQLCondition(vscheduletime)
                .BLike(vscheduletime.CYCLE_TYPE, pKeyWork)
                .Or()
                .BLike(vscheduletime.RUN_URL, pKeyWork)
                .Or()
                .BLike(vscheduletime.SCHEDULE_NAME, pKeyWork)
                .Or()
                .BLike(vscheduletime.RUN_URL_PARAMETER, pKeyWork)
                ).FetchCount();


            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public IList<VScheduleTime_Record> getVScheduleTime_By_Keyword(string pKeyWork, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.VScheduleTime vscheduletime = new LKWebTemplate.Model.Basic.Table.VScheduleTime(dbc);
                return vscheduletime.Where(new SQLCondition(vscheduletime)
                .BLike(vscheduletime.CYCLE_TYPE, pKeyWork)
                .Or()
                .BLike(vscheduletime.RUN_URL, pKeyWork)
                .Or()
                .BLike(vscheduletime.SCHEDULE_NAME, pKeyWork)
                .Or()
                .BLike(vscheduletime.RUN_URL_PARAMETER, pKeyWork)
                )
                .Limit(orderlimit)
                .FetchAll<VScheduleTime_Record>();


            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<VScheduleTime_Record> getVScheduleTime_By_StartDate(DateTime startDate)
        {
            try
            {
                DateTime s = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
                DateTime e = s.AddMinutes(1).AddSeconds(-1);
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.VScheduleTime vscheduletime = new LKWebTemplate.Model.Basic.Table.VScheduleTime(dbc);
                return vscheduletime.Where(new SQLCondition(vscheduletime)
                .OverEqual(vscheduletime.START_TIME, s)
                .And()
                .LessEqual(vscheduletime.START_TIME, e)
                .And()
                .Equal(vscheduletime.STATUS, "READY")
                )
                .FetchAll<VScheduleTime_Record>();
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public int getSchedule_By_Keyword_Count(string pKeyWork)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Schedule schedule = new LKWebTemplate.Model.Basic.Table.Schedule(dbc);
                return schedule.Where(new SQLCondition(schedule)
                .BLike(schedule.CYCLE_TYPE, pKeyWork)
                .Or()
                .BLike(schedule.RUN_URL, pKeyWork)
                .Or()
                .BLike(schedule.SCHEDULE_NAME, pKeyWork)
                .Or()
                .BLike(schedule.RUN_URL_PARAMETER, pKeyWork)
                ).FetchCount();


            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }


        public IList<Schedule_Record> getSchedule_By_Keyword(string pKeyWork, OrderLimit orderlimit)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Schedule schedule = new LKWebTemplate.Model.Basic.Table.Schedule(dbc);
                return schedule.Where(new SQLCondition(schedule)
                .BLike(schedule.CYCLE_TYPE, pKeyWork)
                .Or()
                .BLike(schedule.RUN_URL, pKeyWork)
                .Or()
                .BLike(schedule.SCHEDULE_NAME, pKeyWork)
                .Or()
                .BLike(schedule.RUN_URL_PARAMETER, pKeyWork)
                )
                .Limit(orderlimit)
                .FetchAll<Schedule_Record>();


            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<Schedule_Record> getSchedule_By_ExpendAll(string pExpandAll)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.Schedule schedule = new LKWebTemplate.Model.Basic.Table.Schedule(dbc);
                return schedule.Where(new SQLCondition(schedule)
                .BLike(schedule.EXPEND_ALL, "N")
                )
                .FetchAll<Schedule_Record>();
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<ScheduleTime_Record> getScheduleTime_By_ScheduleUuid(string pScheduleUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.ScheduleTime scheduletime = new LKWebTemplate.Model.Basic.Table.ScheduleTime(dbc);
                return scheduletime.Where(new SQLCondition(scheduletime)
                .Equal(scheduletime.SCHEDULE_UUID, pScheduleUuid))
                .FetchAll<ScheduleTime_Record>();

            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<AttendantV_Record> getAttendantV_By_Company_Account_Password_FormDomain(string pCompanyName, string pAccount, string pPassword, string pDomain)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                var attendantv = new AttendantV(dbc);
                var result = attendantv.Where(new SQLCondition(attendantv)
                                                .Equal(attendantv.COMPANY_ID, pCompanyName, false)
                                                .And()
                                                .Equal(attendantv.ACCOUNT, pAccount, false)
                                                ).FetchAll<AttendantV_Record>();
                bool isValid = false;
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, pDomain))
                {
                    // validate the credentials
                    isValid = pc.ValidateCredentials(pAccount, pPassword);
                }
                if (isValid)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Domain認證失敗");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAttendantV_Record> getGroupAttendantVByAttendantUuid
          (string attendant_uuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAttendantV table = new LKWebTemplate.Model.Basic.Table.GroupAttendantV(dbc);

                SQLCondition con = new SQLCondition(table)
                    .Equal(table.ATTENDANT_UUID, attendant_uuid);

                var result = table.Where(con).FetchAll<GroupAttendantV_Record>();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

        public IList<GroupAppmenu_Record> getGroupAppmenu_By_AppMenuUuid(string pAppMenuUuid)
        {
            try
            {
                dbc = LK.Config.DataBase.Factory.getInfo();
                LKWebTemplate.Model.Basic.Table.GroupAppmenu groupappmenu = new LKWebTemplate.Model.Basic.Table.GroupAppmenu(dbc);
                return groupappmenu.Where(new SQLCondition(groupappmenu)
                    .Equal(groupappmenu.APPMENU_UUID, pAppMenuUuid)
                    ).FetchAll<GroupAppmenu_Record>();
            }
            catch (Exception ex)
            {
                log.Error(ex); LK.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }

    }
}

