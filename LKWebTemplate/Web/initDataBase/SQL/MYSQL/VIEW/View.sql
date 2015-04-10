CREATE 
         
    
VIEW `group_appmenu_v` AS
    select 
        `a`.`uuid` AS `UUID`,
        `a`.`is_active` AS `IS_ACTIVE`,
        `a`.`create_date` AS `CREATE_DATE`,
        `a`.`create_user` AS `CREATE_USER`,
        `a`.`update_date` AS `UPDATE_DATE`,
        `a`.`update_user` AS `UPDATE_USER`,
        `a`.`appmenu_uuid` AS `APPMENU_UUID`,
        `a`.`group_head_uuid` AS `GROUP_HEAD_UUID`,
        `a`.`is_default_page` AS `IS_DEFAULT_PAGE`
    from
        `group_appmenu` `a`;

CREATE 
     
    
    
VIEW `department_v` AS
    select 
        `cmp`.`id` AS `company_id`,
        `cmp`.`c_name` AS `company_c_name`,
        `a`.`uuid` AS `UUID`,
        `a`.`create_date` AS `CREATE_DATE`,
        `a`.`update_date` AS `UPDATE_DATE`,
        `a`.`is_active` AS `IS_ACTIVE`,
        `a`.`company_uuid` AS `COMPANY_UUID`,
        `a`.`id` AS `ID`,
        `a`.`c_name` AS `C_NAME`,
        `a`.`e_name` AS `E_NAME`,
        `a`.`parent_department_uuid` AS `PARENT_DEPARTMENT_UUID`,
        `a`.`manager_uuid` AS `MANAGER_UUID`,
        `a`.`parent_department_id` AS `PARENT_DEPARTMENT_ID`,
        `a`.`manager_id` AS `MANAGER_ID`,
        `a`.`parent_department_uuid_list` AS `PARENT_DEPARTMENT_UUID_LIST`,
        `a`.`s_name` AS `S_NAME`,
        `a`.`cost_center` AS `COST_CENTER`,
        `a`.`src_uuid` AS `SRC_UUID`
    from
        (`department` `a`
        join `company` `cmp`)
    where
        (`a`.`company_uuid` = `cmp`.`uuid`);
		
CREATE 
     
    
    
VIEW `sitemap_v` AS
    select 
        `si`.`uuid` AS `UUID`,
        `si`.`is_active` AS `IS_ACTIVE`,
        `si`.`create_date` AS `CREATE_DATE`,
        `si`.`create_user` AS `CREATE_USER`,
        `si`.`update_date` AS `UPDATE_DATE`,
        `si`.`update_user` AS `UPDATE_USER`,
        `si`.`sitemap_uuid` AS `SITEMAP_UUID`,
        `si`.`apppage_uuid` AS `APPPAGE_UUID`,
        `si`.`root_uuid` AS `ROOT_UUID`,
        `si`.`haschild` AS `HASCHILD`,
        `si`.`application_head_uuid` AS `APPLICATION_HEAD_UUID`,
        `ap`.`name` AS `name`,
        `ap`.`description` AS `description`,
        `ap`.`url` AS `url`,
        `ap`.`p_mode` AS `p_mode`,
        `ap`.`parameter_class` AS `parameter_class`,
        `ap`.`is_active` AS `apppage_is_active`
    from
        (`sitemap` `si`
        join `apppage` `ap`)
    where
        ((`si`.`apppage_uuid` = `ap`.`uuid`)
            and (`si`.`application_head_uuid` = `ap`.`application_head_uuid`))
;
CREATE 
     
    
    
VIEW `authority_url_v` AS
    select distinct
        `ap`.`url` AS `url`,
        `ap`.`application_head_uuid` AS `application_head_uuid`
    from
        (`sitemap` `si`
        join `apppage` `ap`)
    where
        ((`si`.`is_active` = 'Y')
            and (`ap`.`is_active` = 'Y')
            and (`si`.`apppage_uuid` = `ap`.`uuid`)
            and (`si`.`application_head_uuid` = `ap`.`application_head_uuid`))
;
CREATE 
     
    
    
VIEW `vw_department_query` AS
    select 
        `c`.`c_name` AS `company_c_name`,
        `c`.`uuid` AS `company_uuid`,
        `c`.`e_name` AS `company_e_name`,
        `emp`.`account` AS `manager_account`,
        `emp`.`c_name` AS `manager_c_name`,
        `d`.`full_department_name` AS `full_department_name`,
        `d`.`uuid` AS `uuid`,
        `d`.`id` AS `id`,
        `d`.`c_name` AS `c_name`,
        `d`.`e_name` AS `e_name`,
        `d`.`parent_department_uuid` AS `parent_department_uuid`,
        `d`.`manager_uuid` AS `manager_uuid`,
        `d`.`parent_department_id` AS `parent_department_id`,
        `d`.`manager_id` AS `manager_id`,
        `d`.`s_name` AS `s_name`,
        `d`.`cost_center` AS `cost_center`,
        `d`.`src_uuid` AS `src_uuid`
    from
        ((`department` `d`
        left join `company` `c` ON ((`d`.`company_uuid` = `c`.`uuid`)))
        left join `attendant` `emp` ON ((`emp`.`uuid` = `d`.`manager_uuid`)))
;
CREATE 
     
    
    
VIEW `vw_attendant_query` AS
    select 
        `a`.`uuid` AS `uuid`,
        `a`.`create_date` AS `create_date`,
        `a`.`update_date` AS `update_date`,
        `a`.`is_active` AS `is_active`,
        `a`.`company_uuid` AS `company_uuid`,
        `a`.`account` AS `account`,
        `a`.`c_name` AS `c_name`,
        `a`.`e_name` AS `e_name`,
        `a`.`email` AS `email`,
        `a`.`password` AS `password`,
        `a`.`is_supper` AS `is_supper`,
        `a`.`is_admin` AS `is_admin`,
        `a`.`code_page` AS `code_page`,
        `a`.`department_uuid` AS `department_uuid`,
        `d`.`c_name` AS `department_c_name`,
        `a`.`phone` AS `phone`,
        `a`.`site_uuid` AS `site_uuid`,
        `a`.`gender` AS `gender`,
        `a`.`birthday` AS `birthday`,
        `a`.`hire_date` AS `hire_date`,
        `a`.`quit_date` AS `quit_date`,
        `a`.`is_manager` AS `is_manager`,
        `a`.`is_direct` AS `is_direct`,
        `a`.`grade` AS `grade`,
        `a`.`id` AS `id`,
        `a`.`src_uuid` AS `src_uuid`
    from
        (`attendant` `a`
        left join `department` `d` ON ((`a`.`department_uuid` = `d`.`uuid`)))
;
CREATE 
     
    
    
VIEW `group_attendant_v` AS
    select 
        `gp_h`.`name_zh_tw` AS `group_name_zh_tw`,
        `gp_h`.`name_zh_cn` AS `group_name_zh_cn`,
        `gp_h`.`name_en_us` AS `group_name_en_us`,
        `gp_h`.`is_active` AS `is_group_active`,
        `att`.`company_uuid` AS `company_uuid`,
        `cmp`.`id` AS `company_id`,
        `cmp`.`c_name` AS `company_c_name`,
        `cmp`.`e_name` AS `company_e_name`,
        `gp_h`.`id` AS `group_id`,
        `gp_h`.`application_head_uuid` AS `application_head_uuid`,
        `att`.`c_name` AS `attendant_c_name`,
        `att`.`e_name` AS `attendant_E_name`,
        `att`.`account` AS `account`,
        `att`.`email` AS `email`,
        `att`.`is_active` AS `is_attendant_active`,
        `gp_a`.`uuid` AS `UUID`,
        `gp_a`.`create_date` AS `CREATE_DATE`,
        `gp_a`.`update_date` AS `UPDATE_DATE`,
        `gp_a`.`is_active` AS `IS_ACTIVE`,
        `gp_a`.`group_head_uuid` AS `group_head_uuid`,
        `gp_a`.`attendant_uuid` AS `attendant_uuid`,
        `att`.`department_uuid` AS `DEPARTMENT_UUID`
    from
        (((`group_head` `gp_h`
        join `group_attendant` `gp_a`)
        join `attendant` `att`)
        join `company` `cmp`)
    where
        ((`gp_h`.`uuid` = `gp_a`.`group_head_uuid`)
            and (`gp_a`.`attendant_uuid` = `att`.`uuid`)
            and (`att`.`company_uuid` = `cmp`.`uuid`)
            and (`gp_h`.`is_active` = 'Y')
            and (`gp_a`.`is_active` = 'Y')
            and (`att`.`is_active` = 'Y')
            and (`cmp`.`is_active` = 'Y'))
;
CREATE VIEW company_v
	as 
	SELECT
			 A.UUID,
			 A.CREATE_DATE,
			 A.UPDATE_DATE,
			 A.IS_ACTIVE,
			 A.CLASS,
			 A.ID,
			 A.C_NAME,
			 A.E_NAME,
			 A.VOUCHER_POINT_UUID,
			 A.WEEK_SHIFT,
			 A.OU_SYNC_TYPE,
			 A.NAME_ZH_CN,
			 A.CONCURRENT_USER,
			 A.EXPIRED_DATE,
			 A.SALES_ATTENDANT_UUID,
			 atten.account sales_account,
			 atten.c_name sales_c_name,
			 atten.e_name sales_e_name,
			 atten.email sales_email
	FROM  company A  
	LEFT OUTER JOIN  attendant ATTEN  ON  a.sales_attendant_uuid  = atten.uuid
;
CREATE 
     
    
    
VIEW `authority_logon_v` AS
    select 
        `cmp`.`id` AS `company_id`,
        `cmp`.`c_name` AS `company_c_name`,
        `cmp`.`e_name` AS `company_e_name`,
        `dep`.`id` AS `department_id`,
        `dep`.`c_name` AS `department_c_name`,
        `dep`.`e_name` AS `department_e_name`,
        `sit`.`id` AS `site_id`,
        `sit`.`c_name` AS `site_c_name`,
        `sit`.`e_name` AS `site_e_name`,
        (case `att`.`c_name`
            when 'ANONYMOUS' then ' '
            else (((((`att`.`c_name` + '(') + `att`.`e_name`) + ')(') + `cmp`.`id`) + ')')
        end) AS `login_info`,
        `att`.`uuid` AS `attendant_uuid`,
        `att`.`uuid` AS `UUID`,
        `att`.`create_date` AS `CREATE_DATE`,
        `att`.`update_date` AS `UPDATE_DATE`,
        `att`.`is_active` AS `IS_ACTIVE`,
        `att`.`company_uuid` AS `COMPANY_UUID`,
        `att`.`account` AS `ACCOUNT`,
        `att`.`c_name` AS `C_NAME`,
        `att`.`e_name` AS `E_NAME`,
        `att`.`email` AS `EMAIL`,
        `att`.`password` AS `PASSWORD`,
        `att`.`is_supper` AS `IS_SUPPER`,
        `att`.`is_admin` AS `IS_ADMIN`,
        `att`.`code_page` AS `CODE_PAGE`,
        `att`.`department_uuid` AS `DEPARTMENT_UUID`,
        `att`.`phone` AS `PHONE`,
        `att`.`site_uuid` AS `SITE_UUID`,
        `att`.`gender` AS `GENDER`,
        `att`.`birthday` AS `BIRTHDAY`,
        `att`.`hire_date` AS `HIRE_DATE`,
        `att`.`quit_date` AS `QUIT_DATE`,
        `att`.`is_manager` AS `IS_MANAGER`,
        `att`.`is_direct` AS `IS_DIRECT`,
        `att`.`grade` AS `GRADE`,
        `att`.`id` AS `ID`,
        `att`.`src_uuid` AS `SRC_UUID`
    from
        (((`attendant` `att`
        left join `department` `dep` ON ((`att`.`department_uuid` = `dep`.`uuid`)))
        left join `site` `sit` ON ((`att`.`site_uuid` = `sit`.`uuid`)))
        join `company` `cmp`)
    where
        ((`att`.`company_uuid` = `cmp`.`uuid`)
            and (`att`.`is_active` = 'Y')
            and (`cmp`.`is_active` = 'Y'))
;
CREATE 
     
    
    
VIEW `attendant_v` AS
    select 
        `cmp`.`id` AS `company_id`,
        `cmp`.`c_name` AS `company_c_name`,
        `cmp`.`e_name` AS `company_e_name`,
        `dep`.`id` AS `department_id`,
        `dep`.`c_name` AS `department_c_name`,
        `dep`.`e_name` AS `department_e_name`,
        `sit`.`id` AS `site_id`,
        `sit`.`c_name` AS `site_c_name`,
        `sit`.`e_name` AS `site_e_name`,
        `a`.`uuid` AS `UUID`,
        `a`.`create_date` AS `CREATE_DATE`,
        `a`.`update_date` AS `UPDATE_DATE`,
        `a`.`is_active` AS `IS_ACTIVE`,
        `a`.`company_uuid` AS `COMPANY_UUID`,
        `a`.`account` AS `ACCOUNT`,
        `a`.`c_name` AS `C_NAME`,
        `a`.`e_name` AS `E_NAME`,
        `a`.`email` AS `EMAIL`,
        `a`.`password` AS `PASSWORD`,
        `a`.`is_supper` AS `IS_SUPPER`,
        `a`.`is_admin` AS `IS_ADMIN`,
        `a`.`code_page` AS `CODE_PAGE`,
        `a`.`department_uuid` AS `DEPARTMENT_UUID`,
        `a`.`phone` AS `PHONE`,
        `a`.`site_uuid` AS `SITE_UUID`,
        `a`.`gender` AS `GENDER`,
        `a`.`birthday` AS `BIRTHDAY`,
        `a`.`hire_date` AS `HIRE_DATE`,
        `a`.`quit_date` AS `QUIT_DATE`,
        `a`.`is_manager` AS `IS_MANAGER`,
        `a`.`is_direct` AS `IS_DIRECT`,
        `a`.`grade` AS `GRADE`,
        `a`.`id` AS `ID`,
        `a`.`is_default_pass` AS `IS_DEFAULT_PASS`,
        `a`.`picture_url` AS `PICTURE_URL`
    from
        (((`attendant` `a`
        left join `department` `dep` ON ((`a`.`department_uuid` = `dep`.`uuid`)))
        left join `site` `sit` ON ((`a`.`site_uuid` = `sit`.`uuid`)))
        join `company` `cmp`)
    where
        (`a`.`company_uuid` = `cmp`.`uuid`)
;
CREATE 
     
    
    
VIEW `apppage_v` AS
    select 
        `apppage`.`uuid` AS `UUID`,
        `apppage`.`is_active` AS `IS_ACTIVE`,
        `apppage`.`create_date` AS `CREATE_DATE`,
        `apppage`.`create_user` AS `CREATE_USER`,
        `apppage`.`update_date` AS `UPDATE_DATE`,
        `apppage`.`update_user` AS `UPDATE_USER`,
        `apppage`.`id` AS `ID`,
        `apppage`.`name` AS `NAME`,
        `apppage`.`description` AS `DESCRIPTION`,
        `apppage`.`url` AS `URL`,
        `apppage`.`parameter_class` AS `PARAMETER_CLASS`,
        `apppage`.`application_head_uuid` AS `APPLICATION_HEAD_UUID`,
        `apppage`.`p_mode` AS `P_MODE`
    from
        `apppage`
;
CREATE 
     
    
    
VIEW `other_authority_application_v` AS
    select distinct
        `app`.`name` AS `Application_Name`,
        `app`.`id` AS `Application_Id`,
        `gp_h`.`application_head_uuid` AS `application_head_uuid`,
        `att`.`c_name` AS `attendant_c_name`,
        `att`.`e_name` AS `attendant_e_name`,
        `gp_a`.`attendant_uuid` AS `attendant_uuid`,
        `att`.`department_uuid` AS `department_uuid`
    from
        (((`application_head` `app`
        join `group_head` `gp_h`)
        join `group_attendant` `gp_a`)
        join `attendant` `att`)
    where
        ((`app`.`is_active` = 'Y')
            and (`gp_h`.`is_active` = 'Y')
            and (`gp_a`.`is_active` = 'Y')
            and (`att`.`is_active` = 'Y')
            and (`gp_h`.`application_head_uuid` = `app`.`uuid`)
            and (`gp_a`.`group_head_uuid` = `gp_h`.`uuid`)
            and (`gp_a`.`attendant_uuid` = `att`.`uuid`))
;

CREATE 
     
    
    
VIEW `authority_sitemap_v_sub1` AS
    select distinct
        `m`.`uuid` AS `uuid`,
        `m`.`is_active` AS `is_active`,
        `m`.`create_date` AS `create_date`,
        `m`.`create_user` AS `create_user`,
        `m`.`update_date` AS `update_date`,
        `m`.`update_user` AS `update_user`,
        `m`.`name_zh_tw` AS `name_zh_tw`,
        `m`.`name_zh_cn` AS `name_zh_cn`,
        `m`.`name_en_us` AS `name_en_us`,
        `m`.`id` AS `id`,
        `m`.`appmenu_uuid` AS `appmenu_uuid`,
        `m`.`haschild` AS `haschild`,
        `m`.`application_head_uuid` AS `application_head_uuid`,
        `m`.`ord` AS `ord`,
        `m`.`parameter_class` AS `parameter_class`,
        `m`.`image` AS `image`,
        `m`.`sitemap_uuid` AS `sitemap_uuid`,
        `m`.`action_mode` AS `action_mode`,
        `m`.`is_default_page` AS `is_default_page`,
        `m`.`is_admin` AS `is_admin`,
        `gp_m`.`is_default_page` AS `is_user_default_page`,
        `gp_a`.`attendant_uuid` AS `attendant_uuid`
    from
        (((`group_appmenu` `gp_m`
        join `group_head` `gp_h`)
        join `group_attendant` `gp_a`)
        join `appmenu` `m`)
    where
        ((`gp_m`.`group_head_uuid` = `gp_h`.`uuid`)
            and (`gp_h`.`uuid` = `gp_a`.`group_head_uuid`)
            and (`gp_m`.`appmenu_uuid` = `m`.`uuid`)
            and (`m`.`is_active` = 'Y')
            and (`gp_m`.`is_active` = 'Y')
            and (`gp_h`.`is_active` = 'Y')
            and (`gp_a`.`is_active` = 'Y'))
;
CREATE 
     
    
    
VIEW `authority_sitemap_v` AS
    select 
        `p`.`url` AS `Url`,
        `p`.`parameter_class` AS `parameter_class`,
        `p`.`p_mode` AS `p_mode`,
        `p`.`application_head_uuid` AS `application_head_uuid`,
        `si`.`root_uuid` AS `sitemap_root_uuid`,
        `p`.`uuid` AS `uuid`,
        `p`.`is_active` AS `is_active`,
        `p`.`create_date` AS `create_date`,
        `p`.`create_user` AS `create_user`,
        `p`.`update_date` AS `update_date`,
        `p`.`update_user` AS `update_user`,
        `auth_m`.`parameter_class` AS `menu_parameter_class`,
        `auth_m`.`action_mode` AS `ACTION_MODE`,
        `auth_m`.`is_admin` AS `is_admin`,
        `auth_m`.`attendant_uuid` AS `attendant_uuid`
    from
        ((`apppage` `p`
        join `sitemap` `si`)
        join `authority_sitemap_v_sub1` `auth_m`)
    where
        ((`si`.`apppage_uuid` = `p`.`uuid`)
            and (`p`.`application_head_uuid` = `auth_m`.`application_head_uuid`)
            and (`si`.`application_head_uuid` = `auth_m`.`application_head_uuid`)
            and (`si`.`root_uuid` = `auth_m`.`sitemap_uuid`)
            and (`p`.`is_active` = 'Y')
            and (`si`.`is_active` = 'Y'))
;
CREATE 
     
    
    
VIEW `application_head_v` AS
    select 
        `application_head`.`uuid` AS `UUID`,
        `application_head`.`create_date` AS `CREATE_DATE`,
        `application_head`.`update_date` AS `UPDATE_DATE`,
        `application_head`.`is_active` AS `IS_ACTIVE`,
        `application_head`.`name` AS `NAME`,
        `application_head`.`description` AS `DESCRIPTION`,
        `application_head`.`id` AS `ID`,
        `application_head`.`create_user` AS `CREATE_USER`,
        `application_head`.`update_user` AS `UPDATE_USER`,
        `application_head`.`web_site` AS `WEB_SITE`
    from
        `application_head`
;

CREATE 
     
    
    
VIEW `group_head_v` AS
    select 
        `gh`.`uuid` AS `uuid`,
        `gh`.`create_date` AS `create_date`,
        `gh`.`update_date` AS `update_date`,
        `gh`.`is_active` AS `is_group_active`,
        `gh`.`name_zh_tw` AS `name_zh_tw`,
        `gh`.`name_zh_cn` AS `name_zh_cn`,
        `gh`.`name_en_us` AS `name_en_us`,
        `gh`.`company_uuid` AS `company_uuid`,
        `gh`.`id` AS `id`,
        `gh`.`create_user` AS `create_user`,
        `gh`.`update_user` AS `update_user`,
        `gh`.`application_head_uuid` AS `application_head_uuid`,
        `ah`.`id` AS `application_id`,
        `ah`.`name` AS `application_name`
    from
        (`group_head` `gh`
        join `application_head` `ah` ON ((`gh`.`application_head_uuid` = `ah`.`uuid`)))
;
CREATE 
     
    
    
VIEW `authority_menu_v_sub1` AS
    select distinct
        `ga`.`attendant_uuid` AS `attendant_uuid`,
        `ap`.`name` AS `application_name`,
        `g`.`application_head_uuid` AS `application_head_uuid`,
        `gm`.`appmenu_uuid` AS `appmenu_uuid`
    from
        (((`group_attendant` `ga`
        join `group_appmenu` `gm`)
        join `group_head` `g`)
        join `application_head` `ap`)
    where
        ((`ga`.`is_active` = 'Y')
            and (`g`.`is_active` = 'Y')
            and (`gm`.`is_active` = 'Y')
            and (`ap`.`is_active` = 'Y')
            and (`ap`.`uuid` = `g`.`application_head_uuid`)
            and (`g`.`uuid` = `ga`.`group_head_uuid`)
            and (`g`.`uuid` = `gm`.`group_head_uuid`))
;
CREATE 
     
    
    
VIEW `authority_menu_v_sub2` AS
    select 
        `m`.`uuid` AS `uuid`,
        `m`.`is_active` AS `is_active`,
        `m`.`create_date` AS `create_date`,
        `m`.`create_user` AS `create_user`,
        `m`.`update_date` AS `update_date`,
        `m`.`update_user` AS `update_user`,
        `m`.`name_zh_tw` AS `name_zh_tw`,
        `m`.`name_zh_cn` AS `name_zh_cn`,
        `m`.`name_en_us` AS `name_en_us`,
        `m`.`name_jpn` AS `name_jpn`,
        `m`.`id` AS `id`,
        `m`.`appmenu_uuid` AS `appmenu_uuid`,
        `m`.`haschild` AS `haschild`,
        `m`.`application_head_uuid` AS `application_head_uuid`,
        `m`.`ord` AS `ord`,
        `m`.`parameter_class` AS `parameter_class`,
        `m`.`image` AS `image`,
        `m`.`sitemap_uuid` AS `sitemap_uuid`,
        `m`.`action_mode` AS `action_mode`,
        `m`.`is_default_page` AS `is_default_page`,
        `m`.`is_admin` AS `is_admin`,
        `b`.`attendant_uuid` AS `attendant_uuid`,
        `b`.`application_name` AS `application_name`
    from
        (`appmenu` `m`
        left join `authority_menu_v_sub1` `b` ON (((`m`.`application_head_uuid` = `b`.`application_head_uuid`)
            and (`m`.`uuid` = `b`.`appmenu_uuid`))))
    where
        (`m`.`is_active` = 'Y')
;
CREATE 
     
    
    
VIEW `authority_menu_v_sub3` AS
    select 
        `si`.`uuid` AS `uuid`,
        `p`.`url` AS `url`,
        `p`.`parameter_class` AS `parameter_class`,
        `p`.`p_mode` AS `p_mode`,
        `p`.`runJsFunction` AS `runjsfunction`,
        `si`.`application_head_uuid` AS `application_head_uuid`
    from
        (`sitemap` `si`
        join `apppage` `p`)
    where
        ((`si`.`apppage_uuid` = `p`.`uuid`)
            and (`si`.`is_active` = 'Y')
            and (`p`.`is_active` = 'Y')
            and (`si`.`application_head_uuid` = `p`.`application_head_uuid`))
;
CREATE 
     
    
    
VIEW `authority_menu_v_sub4` AS
    select distinct
        `gm`.`is_default_page` AS `is_user_default_page`,
        `ap`.`name` AS `application_name`,
        `g`.`application_head_uuid` AS `application_head_uuid`,
        `ga`.`attendant_uuid` AS `attendant_uuid`,
        `gm`.`appmenu_uuid` AS `appmenu_uuid`
    from
        (((`group_attendant` `ga`
        join `group_appmenu` `gm`)
        join `group_head` `g`)
        join `application_head` `ap`)
    where
        ((`ga`.`is_active` = 'Y')
            and (`g`.`is_active` = 'Y')
            and (`gm`.`is_active` = 'Y')
            and (`ap`.`is_active` = 'Y')
            and (`ap`.`uuid` = `g`.`application_head_uuid`)
            and (`g`.`uuid` = `ga`.`group_head_uuid`)
            and (`g`.`uuid` = `gm`.`group_head_uuid`)
            and (`gm`.`is_default_page` = 'Y'))
;
CREATE 
     
    
    
VIEW `authority_menu_v` AS
    select 
        (case
            when isnull(`d`.`is_user_default_page`) then 'N'
            else 'Y'
        end) AS `is_user_default_page`,
        `auth_m`.`uuid` AS `UUID`,
        `auth_m`.`is_active` AS `IS_ACTIVE`,
        `auth_m`.`create_date` AS `CREATE_DATE`,
        `auth_m`.`create_user` AS `CREATE_USER`,
        `auth_m`.`update_date` AS `UPDATE_DATE`,
        `auth_m`.`update_user` AS `UPDATE_USER`,
        `auth_m`.`name_zh_tw` AS `NAME_ZH_TW`,
        `auth_m`.`name_zh_cn` AS `NAME_ZH_CN`,
        `auth_m`.`name_en_us` AS `NAME_EN_US`,
        `auth_m`.`name_jpn` AS `NAME_JPN`,
        `auth_m`.`id` AS `ID`,
        `auth_m`.`appmenu_uuid` AS `APPMENU_UUID`,
        `auth_m`.`haschild` AS `HASCHILD`,
        `auth_m`.`application_head_uuid` AS `APPLICATION_HEAD_UUID`,
        `auth_m`.`ord` AS `ORD`,
        `auth_m`.`parameter_class` AS `PARAMETER_CLASS`,
        `auth_m`.`image` AS `IMAGE`,
        `auth_m`.`sitemap_uuid` AS `SITEMAP_UUID`,
        `auth_m`.`action_mode` AS `ACTION_MODE`,
        `auth_m`.`is_default_page` AS `IS_DEFAULT_PAGE`,
        `auth_m`.`is_admin` AS `IS_ADMIN`,
        `auth_m`.`attendant_uuid` AS `ATTENDANT_UUID`,
        `auth_m`.`application_name` AS `APPLICATION_NAME`,
        `x`.`url` AS `url`,
        `x`.`parameter_class` AS `func_parameter_class`,
        `x`.`p_mode` AS `p_mode`,
        `x`.`runjsfunction` AS `runjsfunction`
    from
        ((`authority_menu_v_sub2` `auth_m`
        left join `authority_menu_v_sub3` `x` ON (((`auth_m`.`sitemap_uuid` = `x`.`uuid`)
            and (`auth_m`.`application_head_uuid` = `x`.`application_head_uuid`))))
        left join `authority_menu_v_sub4` `d` ON (((`auth_m`.`uuid` = `d`.`appmenu_uuid`)
            and (`auth_m`.`application_head_uuid` = `d`.`application_head_uuid`)
            and (`auth_m`.`attendant_uuid` = `d`.`attendant_uuid`))))
;CREATE 
     
    
    
VIEW `authority_overview_v_sub1` AS
    select distinct
        `m`.`uuid` AS `uuid`,
        `m`.`is_active` AS `is_active`,
        `m`.`create_date` AS `create_date`,
        `m`.`create_user` AS `create_user`,
        `m`.`update_date` AS `update_date`,
        `m`.`update_user` AS `update_user`,
        `m`.`name_zh_tw` AS `name_zh_tw`,
        `m`.`name_zh_cn` AS `name_zh_cn`,
        `m`.`name_en_us` AS `name_en_us`,
        `m`.`id` AS `id`,
        `m`.`appmenu_uuid` AS `appmenu_uuid`,
        `m`.`haschild` AS `haschild`,
        `m`.`application_head_uuid` AS `application_head_uuid`,
        `m`.`ord` AS `ord`,
        `m`.`parameter_class` AS `parameter_class`,
        `m`.`image` AS `image`,
        `m`.`sitemap_uuid` AS `sitemap_uuid`,
        `m`.`action_mode` AS `action_mode`,
        `m`.`is_default_page` AS `is_default_page`,
        `m`.`is_admin` AS `is_admin`,
        `gp_m`.`is_default_page` AS `is_user_default_page`,
        `gp_a`.`attendant_uuid` AS `attendant_uuid`
    from
        (((`group_appmenu` `gp_m`
        join `group_head` `gp_h`)
        join `group_attendant` `gp_a`)
        join `appmenu` `m`)
    where
        ((`gp_m`.`group_head_uuid` = `gp_h`.`uuid`)
            and (`gp_h`.`uuid` = `gp_a`.`group_head_uuid`)
            and (`gp_m`.`appmenu_uuid` = `m`.`uuid`)
            and (`m`.`is_active` = 'Y')
            and (`gp_m`.`is_active` = 'Y')
            and (`gp_h`.`is_active` = 'Y')
            and (`gp_a`.`is_active` = 'Y'))
;
CREATE 
     
    
    
VIEW `authority_overview_v` AS
    select distinct
        `p`.`url` AS `Url`,
        `p`.`parameter_class` AS `parameter_class`,
        `p`.`description` AS `description`,
        `p`.`p_mode` AS `p_mode`,
        `p`.`application_head_uuid` AS `application_head_uuid`,
        `p`.`is_active` AS `is_active`,
        `p`.`create_date` AS `create_date`,
        `p`.`create_user` AS `create_user`,
        `p`.`update_date` AS `update_date`,
        `p`.`update_user` AS `update_user`,
        `auth_m`.`action_mode` AS `ACTION_MODE`,
        `auth_m`.`attendant_uuid` AS `attendant_uuid`
    from
        ((`apppage` `p`
        join `sitemap` `si`)
        join `authority_overview_v_sub1` `auth_m`)
    where
        ((`si`.`apppage_uuid` = `p`.`uuid`)
            and (`p`.`application_head_uuid` = `auth_m`.`application_head_uuid`)
            and (`si`.`application_head_uuid` = `auth_m`.`application_head_uuid`)
            and (`si`.`root_uuid` = `auth_m`.`sitemap_uuid`)
            and (`p`.`is_active` = 'Y')
            and (`si`.`is_active` = 'Y'))
;
CREATE 
     
    
    
VIEW `appmenu_apppage_v_sub1` AS
    select 
        `si`.`uuid` AS `uuid`,
        `page`.`description` AS `description`,
        `page`.`application_head_uuid` AS `application_head_uuid`,
        replace(`page`.`url`, '/', '/ ') AS `url`,
        `page`.`name` AS `func_name`,
        `page`.`parameter_class` AS `func_parameter_class`,
        `page`.`p_mode` AS `p_mode`
    from
        (`sitemap` `si`
        join `apppage` `page`)
    where
        ((`si`.`application_head_uuid` = `page`.`application_head_uuid`)
            and (`si`.`is_active` = 'Y')
            and (`page`.`is_active` = 'Y')
            and (`si`.`apppage_uuid` = `page`.`uuid`))
;
CREATE VIEW `appmenu_apppage_v` AS
    select 
        `am`.`uuid` AS `UUID`,
        `am`.`is_active` AS `IS_ACTIVE`,
        `am`.`create_date` AS `CREATE_DATE`,
        `am`.`create_user` AS `CREATE_USER`,
        `am`.`update_date` AS `UPDATE_DATE`,
        `am`.`update_user` AS `UPDATE_USER`,
        `am`.`name_zh_tw` AS `NAME_ZH_TW`,
        `am`.`name_zh_cn` AS `NAME_ZH_CN`,
        `am`.`name_en_us` AS `NAME_EN_US`,
        `am`.`name_jpn` AS `NAME_JPN`,
        `am`.`id` AS `ID`,
        `am`.`appmenu_uuid` AS `APPMENU_UUID`,
        `am`.`haschild` AS `HASCHILD`,
        `am`.`application_head_uuid` AS `APPLICATION_HEAD_UUID`,
        `am`.`ord` AS `ORD`,
        `am`.`parameter_class` AS `PARAMETER_CLASS`,
        `am`.`image` AS `IMAGE`,
        `am`.`sitemap_uuid` AS `SITEMAP_UUID`,
        `am`.`action_mode` AS `ACTION_MODE`,
        `am`.`is_default_page` AS `IS_DEFAULT_PAGE`,
        `am`.`is_admin` AS `IS_ADMIN`,
        `x`.`url` AS `url`,
        `x`.`description` AS `description`,
        `x`.`func_name` AS `func_name`,
        `x`.`func_parameter_class` AS `func_parameter_class`,
        `x`.`p_mode` AS `p_mode`
    from
        (`appmenu` `am`
        left join `appmenu_apppage_v_sub1` `x` ON (((`am`.`sitemap_uuid` = `x`.`uuid`)
            and (`am`.`application_head_uuid` = `x`.`application_head_uuid`))))
    where
        (`am`.`is_active` = 'Y')
;
CREATE VIEW `appmenu_v` AS
    select 
        parentlst(`am`.`uuid`) AS `parentlst`,
        `am`.`uuid` AS `UUID`,
        `am`.`is_active` AS `IS_ACTIVE`,
        `am`.`create_date` AS `CREATE_DATE`,
        `am`.`create_user` AS `CREATE_USER`,
        `am`.`update_date` AS `UPDATE_DATE`,
        `am`.`update_user` AS `UPDATE_USER`,
        `am`.`name_zh_tw` AS `NAME_ZH_TW`,
        `am`.`name_zh_cn` AS `NAME_ZH_CN`,
        `am`.`name_en_us` AS `NAME_EN_US`,
        `am`.`name_jpn` AS `NAME_JPN`,
        `am`.`id` AS `ID`,
        `am`.`appmenu_uuid` AS `APPMENU_UUID`,
        `am`.`haschild` AS `HASCHILD`,
        `am`.`application_head_uuid` AS `APPLICATION_HEAD_UUID`,
        `am`.`ord` AS `ORD`,
        `am`.`parameter_class` AS `PARAMETER_CLASS`,
        `am`.`image` AS `IMAGE`,
        `am`.`sitemap_uuid` AS `SITEMAP_UUID`,
        `am`.`action_mode` AS `ACTION_MODE`,
        `am`.`is_default_page` AS `IS_DEFAULT_PAGE`,
        `am`.`is_admin` AS `IS_ADMIN`
    from
        `appmenu` `am`
;


CREATE VIEW `v_appmenu_proxy_map`
AS
SELECT     a.UUID AS proxy_uuid, a.Proxy_Action, a.Proxy_Method, a.description AS proxy_description, a.Proxy_Type, a.need_redirect, a.redirect_Proxy_Action, 
                      a.redirect_Proxy_Method, a.redirect_src, a.application_head_uuid, c.name_zh_tw, c.name_zh_cn, c.name_en_us,c.name_jpn, c.uuid, b.uuid AS appmenu_proxy_uuid, 
                      b.appMenu_uuid AS appmenu_uuid					  
FROM         Proxy AS a INNER JOIN
                      APPMENU_PROXY_MAP AS b ON a.UUID = b.proxy_uuid INNER JOIN
                      APPMENU AS c ON b.appMenu_uuid = c.uuid
;
CREATE VIEW `v_auth_proxy`
AS
SELECT     auth.is_user_default_page, auth.UUID, auth.IS_ACTIVE, auth.CREATE_DATE, auth.CREATE_USER, auth.UPDATE_DATE, auth.UPDATE_USER, auth.NAME_ZH_TW, 
                      auth.NAME_ZH_CN, auth.NAME_EN_US,auth.NAME_JPN, auth.ID, auth.APPMENU_UUID, auth.HASCHILD, auth.APPLICATION_HEAD_UUID, auth.ORD, auth.PARAMETER_CLASS, 
                      auth.IMAGE, auth.SITEMAP_UUID, auth.ACTION_MODE, auth.IS_DEFAULT_PAGE, auth.IS_ADMIN, auth.ATTENDANT_UUID, auth.APPLICATION_NAME, auth.url, 
                      auth.func_parameter_class, auth.p_mode, proxy.proxy_uuid, proxy.Proxy_Action, proxy.Proxy_Method, proxy.proxy_description, proxy.Proxy_Type, 
                      proxy.need_redirect, proxy.redirect_Proxy_Action, proxy.redirect_Proxy_Method, proxy.redirect_src, proxy.appmenu_proxy_uuid
FROM         `authority_menu_v` AS auth LEFT OUTER JOIN
                      `v_appmenu_proxy_map` AS proxy ON auth.UUID = proxy.appmenu_uuid
;