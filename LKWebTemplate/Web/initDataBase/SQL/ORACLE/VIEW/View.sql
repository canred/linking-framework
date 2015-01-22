CREATE OR REPLACE VIEW {user}.GROUP_APPMENU_V AS
SELECT a.UUID,
          a.IS_ACTIVE,
          a.CREATE_DATE,
          a.CREATE_USER,
          a.UPDATE_DATE,
          a.UPDATE_USER,
          a.APPMENU_UUID,
          a.GROUP_HEAD_UUID,
          a.IS_DEFAULT_PAGE
     FROM group_appmenu a;

create or replace view {user}.department_v as
select Cmp.id company_id,Cmp.c_name company_c_name,
       A.UUID,A.CREATE_DATE,A.UPDATE_DATE,A.IS_ACTIVE,A.COMPANY_UUID,A.ID,A.C_NAME,A.E_NAME,A.PARENT_DEPARTMENT_UUID,A.MANAGER_UUID,A.PARENT_DEPARTMENT_ID,A.MANAGER_ID,A.PARENT_DEPARTMENT_UUID_LIST,A.S_NAME,A.COST_CENTER,A.SRC_UUID
  from department A , company Cmp
  where A.company_uuid=Cmp.uuid;

CREATE OR REPLACE VIEW {user}.SITEMAP_V AS
SELECT si.UUID,
          si.IS_ACTIVE,
          si.CREATE_DATE,
          si.CREATE_USER,
          si.UPDATE_DATE,
          si.UPDATE_USER,
          si.SITEMAP_UUID,
          si.APPPAGE_UUID,
          si.ROOT_UUID,
          si.HASCHILD,
          si.APPLICATION_HEAD_UUID,
          ap.name,
          ap.description,
          ap.url,
          ap.p_mode,
          ap.parameter_class,
          ap.is_active apppage_is_active
     FROM sitemap si, apppage ap
    WHERE     si.apppage_uuid = ap.uuid
          AND si.application_head_uuid = ap.application_head_uuid;

CREATE OR REPLACE VIEW {user}.AUTHORITY_URL_V AS
SELECT DISTINCT ap.url, ap.application_head_uuid
     FROM sitemap si, apppage ap
    WHERE     si.is_active = 'Y'
          AND ap.is_active = 'Y'
          AND si.apppage_uuid = ap.uuid
          AND si.application_head_uuid = ap.application_head_uuid;
		  

create or replace view {user}.vw_department_query as
select c.c_name company_c_name,
c.uuid company_uuid,
c.e_name company_e_name,emp.account manager_account,emp.c_name manager_c_name,
d.full_department_name
,
d.uuid,d.id,d.c_name,d.e_name,d.parent_department_uuid,d.manager_uuid,d.parent_department_id,d.manager_id,d.s_name,d.cost_center,d.src_uuid from department d
left join company c on d.company_uuid = c.uuid
left join attendant emp on emp.uuid = d.manager_uuid;



create or replace view {user}.vw_attendant_query as
select
a.uuid,
a.create_date,
a.update_date,
a.is_active,
a.company_uuid,
a.account,
a.c_name,
a.e_name,
a.email,
a.password,
a.is_supper,
a.is_admin,
a.code_page,
a.department_uuid,
d.c_name department_c_name,
a.phone,
a.site_uuid,
a.gender,
a.birthday,
a.hire_date,
a.quit_date,
a.is_manager,
a.is_direct,
a.grade,
a.id,
a.src_uuid
 from attendant a
left join department d on a.department_uuid = d.uuid;




CREATE OR REPLACE VIEW {user}.GROUP_ATTENDANT_V AS
SELECT                                             --R_hd.class role_class,
                                            --R_hd.class_desc role_class_desc,
         Gp_H.name_zh_tw group_name_zh_tw,
         Gp_H.name_zh_cn group_name_zh_cn,
         Gp_H.name_en_us group_name_en_us,
         Gp_H.is_active is_group_active,
         Att.company_uuid,
         Cmp.id company_id,
         Cmp.c_name company_c_name,
         Cmp.e_name company_e_name,
         Gp_H.id GROUP_ID,
         Gp_H.application_head_uuid,
         Att.c_name attendant_c_name,
         Att.E_name attendant_E_name,
         Att.account,
         Att.email,
         Att.is_active is_attendant_active,
         Gp_A.UUID,
         Gp_A.CREATE_DATE,
         Gp_A.UPDATE_DATE,
         Gp_A.IS_ACTIVE,
         Gp_A.group_head_uuid,
         Gp_A.attendant_uuid,
         --A.SEQ,
         --A.AUTO_SRC,
         --A.IS_BOSS,
         Att.DEPARTMENT_UUID
    FROM group_head Gp_H,
         group_attendant Gp_A,
         attendant Att,
         Company Cmp
   WHERE     Gp_H.uuid = Gp_A.group_head_uuid
         AND Gp_A.attendant_uuid = Att.uuid
         AND Att.company_uuid = Cmp.uuid
         AND Gp_H.is_active = 'Y'
         AND Gp_A.is_active = 'Y'
         --AND Att.is_active = 'Y'
         AND Cmp.is_active = 'Y';
		 
		 

create or replace view {user}.company_v as
SELECT     A.uuid, A.create_date, A.update_date, A.is_active, A.class, A.id, A.c_name, A.e_name, A.voucher_point_uuid, A.week_shift, A.ou_sync_type, A.name_zh_cn, 
                      A.concurrent_user, A.expired_date, A.sales_attendant_uuid, ATTEN.account AS sales_account, ATTEN.c_name AS sales_c_name, ATTEN.e_name AS sales_e_name, 
                      ATTEN.email AS sales_email
FROM         COMPANY A LEFT OUTER JOIN
                      ATTENDANT ATTEN ON A.sales_attendant_uuid = ATTEN.uuid;
					  
  

create or replace view {user}.authority_logon_v as
select Cmp.id company_id,     Cmp.c_name company_c_name,    Cmp.e_name company_e_name,
       Dep.id department_id , Dep.c_name department_c_name, Dep.e_name department_e_name,
       Sit.id site_id ,       Sit.c_name site_c_name,       Sit.e_name site_e_name,
       decode(Att.c_name,'ANONYMOUS',' ',Att.c_name||'('||Att.e_name||')('||Cmp.id||')') login_info,Att.uuid attendant_uuid,
       Att.UUID,Att.CREATE_DATE,Att.UPDATE_DATE,Att.IS_ACTIVE,Att.COMPANY_UUID,Att.ACCOUNT,Att.C_NAME,Att.E_NAME,Att.EMAIL,Att.PASSWORD,Att.IS_SUPPER,Att.IS_ADMIN,Att.CODE_PAGE,Att.DEPARTMENT_UUID,Att.PHONE,Att.SITE_UUID,Att.GENDER,Att.BIRTHDAY,Att.HIRE_DATE,Att.QUIT_DATE,Att.IS_MANAGER,Att.IS_DIRECT,Att.GRADE,Att.ID,Att.SRC_UUID
  from attendant Att , company Cmp , department Dep , site Sit
  where Att.company_uuid=Cmp.uuid
    and Att.is_active='Y' and Cmp.is_active='Y'
    and Att.department_uuid=Dep.uuid(+)
    and Att.site_uuid=Sit.uuid(+);
	

	
create or replace view {user}.attendant_v as
select Cmp.id company_id,     Cmp.c_name company_c_name,    Cmp.e_name company_e_name,
       Dep.id department_id , Dep.c_name department_c_name, Dep.e_name department_e_name,
       Sit.id site_id ,       Sit.c_name site_c_name,       Sit.e_name site_e_name,
       A."UUID",A."CREATE_DATE",A."UPDATE_DATE",A."IS_ACTIVE",A."COMPANY_UUID",A."ACCOUNT",A."C_NAME",A."E_NAME",A."EMAIL",A."PASSWORD",A."IS_SUPPER",A."IS_ADMIN",A."CODE_PAGE",A."DEPARTMENT_UUID",A."PHONE",A."SITE_UUID",A."GENDER",A."BIRTHDAY",A."HIRE_DATE",A."QUIT_DATE",A."IS_MANAGER",A."IS_DIRECT",A."GRADE",A."ID",A."IS_DEFAULT_PASS"
  from attendant A , company Cmp , department Dep , site Sit
  where A.company_uuid=Cmp.uuid
    and A.department_uuid=Dep.uuid(+)
    and A.site_uuid=Sit.uuid(+);
	
	

CREATE OR REPLACE VIEW {user}.APPPAGE_V AS
SELECT UUID,
          IS_ACTIVE,
          CREATE_DATE,
          CREATE_USER,
          UPDATE_DATE,
          UPDATE_USER,
          ID,
          NAME,
          DESCRIPTION,
          URL,
          PARAMETER_CLASS,
          APPLICATION_HEAD_UUID,
          P_MODE
     FROM apppage;

	 

CREATE OR REPLACE VIEW {user}.OTHER_AUTHORITY_APPLICATION_V AS
SELECT DISTINCT App.name Application_Name,
                   App.id Application_Id,
                   Gp_H.application_head_uuid,
                   Att.c_name attendant_c_name,
                   Att.e_name attendant_e_name,
                   Gp_A.attendant_uuid,
                   Att.department_uuid
     FROM application_head App,
          group_head Gp_H,
          group_attendant Gp_A,
          attendant Att
    WHERE     App.is_active = 'Y'
          AND gp_h.is_active = 'Y'
          AND Gp_A.is_active = 'Y'
          AND Att.is_active = 'Y'
          AND Gp_H.application_head_uuid = App.uuid
          AND Gp_A.group_head_uuid = Gp_H.uuid
          AND Gp_A.attendant_uuid = Att.uuid;
		  

CREATE OR REPLACE VIEW {user}.AUTHORITY_SITEMAP_V AS
SELECT P.Url,
          P.parameter_class,
          P.p_mode,
          p.application_head_uuid,
          Si.root_uuid sitemap_root_uuid,
          P.uuid,
          P.is_active,
          P.create_date,
          P.create_user,
          P.update_date,
          P.update_user,
          Auth_M.parameter_class menu_parameter_class,
          Auth_M."ACTION_MODE",
          Auth_M.is_admin,
          Auth_M.attendant_uuid
     FROM apppage P,
          sitemap Si,
          (SELECT DISTINCT
                  M.*,
                  gp_m.is_default_page is_user_default_page,
                  gp_a.attendant_uuid
             FROM group_appmenu Gp_M,
                  group_head Gp_H,
                  group_attendant Gp_A,
                  appmenu M
            WHERE     gp_m.group_head_uuid = Gp_H.uuid
                  AND Gp_H.uuid = Gp_A.group_head_uuid
                  AND Gp_M.appmenu_uuid = M.uuid
                  AND M.is_active = 'Y'
                  AND gp_m.is_active = 'Y'
                  AND Gp_H.is_active = 'Y'
                  AND Gp_A.is_active = 'Y') Auth_M
    WHERE     si.apppage_uuid = P.uuid
          AND P.application_head_uuid = Auth_M.application_head_uuid
          AND Si.application_head_uuid = Auth_M.application_head_uuid
          AND Si.root_uuid = Auth_M.sitemap_uuid
          AND p.is_active = 'Y'
          AND si.is_active = 'Y';

		  
		  
		  

CREATE OR REPLACE VIEW {user}.APPLICATION_HEAD_V AS
SELECT A.UUID,
          A.CREATE_DATE,
          A.UPDATE_DATE,
          A.IS_ACTIVE,
          A.NAME,
          A.DESCRIPTION,
          A.ID,
          A.WEB_SITE,
          A.CREATE_USER,
          A.UPDATE_USER
     FROM application_head A;
	 

CREATE OR REPLACE VIEW {user}.GROUP_HEAD_V AS
SELECT gh.UUID,
          gh.CREATE_DATE,
          gh.UPDATE_DATE,
          gh.IS_ACTIVE,
          gh.NAME_ZH_TW,
          gh.NAME_ZH_CN,
          gh.NAME_EN_US,
          gh.COMPANY_UUID,
          gh.ID,
          gh.CREATE_USER,
          gh.UPDATE_USER,
          gh.APPLICATION_HEAD_UUID,
          ah.id application_id,
          ah.name application_name
     FROM group_head gh
          JOIN application_head ah ON gh.application_head_uuid = ah.uuid;

		  
CREATE OR REPLACE VIEW {user}.AUTHORITY_MENU_V AS
SELECT CASE WHEN d.is_user_default_page IS NULL THEN 'N' ELSE 'Y' END
             is_user_default_page,
          auth_m.UUID,
          auth_m.IS_ACTIVE,
          auth_m.CREATE_DATE,
          auth_m.CREATE_USER,
          auth_m.UPDATE_DATE,
          auth_m.UPDATE_USER,
          auth_m.NAME_ZH_TW,
          auth_m.NAME_ZH_CN,
          auth_m.NAME_EN_US,
          auth_m.ID,
          auth_m.APPMENU_UUID,
          auth_m.HASCHILD,
          auth_m.APPLICATION_HEAD_UUID,
          auth_m.ORD,
          auth_m.PARAMETER_CLASS,
          auth_m.IMAGE,
          auth_m.SITEMAP_UUID,
          auth_m.ACTION_MODE,
          auth_m.IS_DEFAULT_PAGE,
          auth_m.IS_ADMIN,
          auth_m.ATTENDANT_UUID,
          auth_m.APPLICATION_NAME,
          X.url,
          X.parameter_class func_parameter_class,
          X.p_mode
     FROM (SELECT m.*, b.attendant_uuid, b.application_name
             FROM (SELECT DISTINCT ga.attendant_uuid,
                                   ap.name application_name,
                                   g.application_head_uuid,
                                   gm.appmenu_uuid
                     FROM group_attendant ga,
                          group_appmenu gm,
                          group_head g,
                          application_head ap
                    WHERE     ga.is_active = 'Y'
                          AND g.is_active = 'Y'
                          AND gm.is_active = 'Y'
                          AND ap.is_active = 'Y'
                          AND ap.uuid = g.application_head_uuid
                          AND g.uuid = ga.group_head_uuid
                          AND g.uuid = gm.group_head_uuid) b,
                  appmenu m
            WHERE     m.is_active = 'Y'
                  AND m.application_head_uuid = b.application_head_uuid(+)
                  AND m.uuid = b.appmenu_uuid(+)) auth_m,
          (SELECT Si.uuid,
                  P.url,
                  P.parameter_class,
                  P.p_mode,
                  Si.application_head_uuid
             FROM sitemap Si, apppage P
            WHERE     Si.apppage_uuid = P.uuid
                  --AND Si.is_active = 'Y'
                  AND P.is_active = 'Y'
                  AND Si.application_head_uuid = P.application_head_uuid) X,
          (SELECT DISTINCT gm.is_default_page is_user_default_page,
                           ap.name application_name,
                           g.application_head_uuid,
                           ga.attendant_uuid,
                           gm.appmenu_uuid
             FROM group_attendant ga,
                  group_appmenu gm,
                  group_head g,
                  application_head ap
            WHERE     ga.is_active = 'Y'
                  AND g.is_active = 'Y'
                  AND gm.is_active = 'Y'
                  AND ap.is_active = 'Y'
                  AND ap.uuid = g.application_head_uuid
                  AND g.uuid = ga.group_head_uuid
                  AND g.uuid = gm.group_head_uuid
                  AND gm.is_default_page = 'Y') d
    WHERE     auth_m.sitemap_uuid = x.uuid(+)
          AND auth_m.application_head_uuid = x.application_head_uuid(+)
          AND auth_m.uuid = d.appmenu_uuid(+)
          AND auth_m.application_head_uuid = d.application_head_uuid(+)
          AND auth_m.attendant_uuid = d.attendant_uuid(+);
		  
		  

CREATE OR REPLACE VIEW {user}.AUTHORITY_OVERVIEW_V AS
SELECT DISTINCT P.Url,
                   P.parameter_class,
                   P.description,
                   P.p_mode,
                   p.application_head_uuid,
                   P.is_active,
                   P.create_date,
                   P.create_user,
                   P.update_date,
                   P.update_user,
                   Auth_M.ACTION_MODE,
                   Auth_M.attendant_uuid
     FROM apppage P,
          sitemap Si,
          (SELECT DISTINCT
                  M.*,
                  gp_m.is_default_page is_user_default_page,
                  gp_a.attendant_uuid
             FROM group_appmenu Gp_M,
                  group_head Gp_H,
                  group_attendant Gp_A,
                  appmenu M
            WHERE     gp_m.group_head_uuid = Gp_H.uuid
                  AND Gp_H.uuid = Gp_A.group_head_uuid
                  AND Gp_M.appmenu_uuid = M.uuid
                  AND M.is_active = 'Y'
                  AND gp_m.is_active = 'Y'
                  AND Gp_H.is_active = 'Y'
                  AND Gp_A.is_active = 'Y') Auth_M
    WHERE     si.apppage_uuid = P.uuid
          AND P.application_head_uuid = Auth_M.application_head_uuid
          AND Si.application_head_uuid = Auth_M.application_head_uuid
          AND Si.root_uuid = Auth_M.sitemap_uuid
          AND p.is_active = 'Y'
          AND si.is_active = 'Y';
		  

CREATE OR REPLACE VIEW {user}.APPMENU_APPPAGE_V AS
SELECT am.UUID,
          am.IS_ACTIVE,
          am.CREATE_DATE,
          am.CREATE_USER,
          am.UPDATE_DATE,
          am.UPDATE_USER,
          am.NAME_ZH_TW,
          am.NAME_ZH_CN,
          am.NAME_EN_US,
          am.ID,
          am.APPMENU_UUID,
          am.HASCHILD,
          am.APPLICATION_HEAD_UUID,
          am.ORD,
          am.PARAMETER_CLASS,
          am.IMAGE,
          am.SITEMAP_UUID,
          am.ACTION_MODE,
          am.IS_DEFAULT_PAGE,
          am.IS_ADMIN,
          x.url,
          x.description,
          x.func_name,
          x.func_parameter_class,
          x.p_mode
     FROM appmenu am,
          (SELECT si.uuid,
                  page.description,
                  page.application_head_uuid,
                  REPLACE (page.url, '/', '/ ') url,
                  page.name func_name,
                  page.parameter_class func_parameter_class,
                  page.p_mode
             FROM sitemap si, apppage page
            WHERE     si.application_head_uuid = page.application_head_uuid
                  AND si.is_active = 'Y'
                  AND page.is_active = 'Y'
                  AND si.apppage_uuid = page.uuid) x
    WHERE     am.sitemap_uuid = x.uuid(+)
          AND am.application_head_uuid = x.application_head_uuid(+)
          AND am.is_active = 'Y';


CREATE OR REPLACE VIEW {user}.APPMENU_V AS
SELECT parentlst (am.uuid) parentlst,
          am.UUID,
          am.IS_ACTIVE,
          am.CREATE_DATE,
          am.CREATE_USER,
          am.UPDATE_DATE,
          am.UPDATE_USER,
          am.NAME_ZH_TW,
          am.NAME_ZH_CN,
          am.NAME_EN_US,
          am.ID,
          am.APPMENU_UUID,
          am.HASCHILD,
          am.APPLICATION_HEAD_UUID,
          am.ORD,
          am.PARAMETER_CLASS,
          am.IMAGE,
          am.SITEMAP_UUID,
          am.ACTION_MODE,
          am.IS_DEFAULT_PAGE,
          am.IS_ADMIN
     FROM appmenu am;
	 
CREATE OR REPLACE VIEW {user}.v_appmenu_proxy_map
AS
SELECT     a.UUID AS proxy_uuid, a.Proxy_Action, a.Proxy_Method, a.description AS proxy_description, a.Proxy_Type, a.need_redirect, a.redirect_Proxy_Action, 
                      a.redirect_Proxy_Method, a.redirect_src, a.application_head_uuid, c.name_zh_tw, c.name_zh_cn, c.name_en_us, c.uuid, b.uuid AS appmenu_proxy_uuid, 
                      b.appMenu_uuid AS appmenu_uuid					  
FROM         Proxy  a INNER JOIN
                      APPMENU_PROXY_MAP  b ON a.UUID = b.proxy_uuid INNER JOIN
                      APPMENU  c ON b.appMenu_uuid = c.uuid
;

CREATE OR REPLACE VIEW {user}.v_auth_proxy
AS
SELECT     auth.is_user_default_page, auth.UUID, auth.IS_ACTIVE, auth.CREATE_DATE, auth.CREATE_USER, auth.UPDATE_DATE, auth.UPDATE_USER, auth.NAME_ZH_TW, 
                      auth.NAME_ZH_CN, auth.NAME_EN_US, auth.ID, auth.APPMENU_UUID, auth.HASCHILD, auth.APPLICATION_HEAD_UUID, auth.ORD, auth.PARAMETER_CLASS, 
                      auth.IMAGE, auth.SITEMAP_UUID, auth.ACTION_MODE, auth.IS_DEFAULT_PAGE, auth.IS_ADMIN, auth.ATTENDANT_UUID, auth.APPLICATION_NAME, auth.url, 
                      auth.func_parameter_class, auth.p_mode, proxy.proxy_uuid, proxy.Proxy_Action, proxy.Proxy_Method, proxy.proxy_description, proxy.Proxy_Type, 
                      proxy.need_redirect, proxy.redirect_Proxy_Action, proxy.redirect_Proxy_Method, proxy.redirect_src, proxy.appmenu_proxy_uuid
FROM         authority_menu_v  auth LEFT OUTER JOIN
                      v_appmenu_proxy_map  proxy ON auth.UUID = proxy.appmenu_uuid
;
	 
