CREATE TABLE `company` (
  `uuid` varchar(36) NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_active` varchar(1) DEFAULT 'Y',
  `class` varchar(50) DEFAULT NULL,
  `id` varchar(100) DEFAULT NULL,
  `c_name` varchar(100) DEFAULT NULL,
  `e_name` varchar(100) DEFAULT NULL,
  `voucher_point_uuid` varchar(36) DEFAULT NULL,
  `week_shift` int(11) DEFAULT '0',
  `ou_sync_type` varchar(1) DEFAULT 'F',
  `name_zh_cn` varchar(128) DEFAULT NULL,
  `concurrent_user` varchar(128) DEFAULT NULL,
  `expired_date` timestamp NULL DEFAULT NULL,
  `sales_attendant_uuid` varchar(36) DEFAULT NULL,
  `is_sync_ad_user` varchar(1) DEFAULT NULL,
  `ad_ldap` varchar(400) DEFAULT NULL,
`ad_ldap_user` varchar(400) DEFAULT NULL,
`ad_ldap_user_password` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `COMPANY_UK1` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `ACTIVE_CONNECTION`(
	`UUID` varchar(36) NOT NULL,
	`ACCOUNT` varchar(40) NOT NULL,
	`COMPANY_UUID` varchar(36) NOT NULL,
	`IP` varchar(20) NOT NULL,
	`APPLICATION` varchar(100) NOT NULL,
	`STARTTIME` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,	
	`EXPIRESTIME` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	`STATUS` varchar(10) NULL,
	PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `department` (
  `uuid` varchar(36) NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `company_uuid` varchar(36) NOT NULL,
  `id` varchar(100) NOT NULL,
  `c_name` varchar(100) DEFAULT NULL,
  `e_name` varchar(100) DEFAULT NULL,
  `parent_department_uuid` varchar(36) DEFAULT NULL,
  `manager_uuid` varchar(36) DEFAULT NULL,
  `parent_department_id` varchar(100) DEFAULT NULL,
  `manager_id` varchar(100) DEFAULT NULL,
  `parent_department_uuid_list` varchar(4000) DEFAULT NULL,
  `s_name` varchar(100) DEFAULT NULL,
  `cost_center` varchar(100) DEFAULT NULL,
  `src_uuid` varchar(36) DEFAULT NULL,
  `full_department_name` varchar(4000) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `DEPARTMENT_UK1` (`company_uuid`,`id`),
  KEY `DEPARTMENT_FK2_idx` (`parent_department_id`),
  KEY `DEPARTMENT_FK3_idx` (`manager_uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
  
  
CREATE TABLE `site` (
  `uuid` varchar(36) NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `company_uuid` varchar(36) NOT NULL,
  `id` varchar(100) NOT NULL,
  `c_name` varchar(100) DEFAULT NULL,
  `e_name` varchar(100) DEFAULT NULL,
  `parent_site_uuid` varchar(36) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `SITE_FK1_idx` (`company_uuid`),
  KEY `SITE_FK2_idx` (`parent_site_uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `attendant` (
  `uuid` varchar(36) NOT NULL,
  `create_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `company_uuid` varchar(36) NOT NULL,
  `account` varchar(50) NOT NULL,
  `c_name` varchar(100) DEFAULT NULL,
  `e_name` varchar(100) DEFAULT NULL,
  `email` varchar(300) NOT NULL,
  `password` varchar(50) DEFAULT NULL,
  `is_supper` varchar(1) NOT NULL DEFAULT 'N',
  `is_admin` varchar(1) NOT NULL DEFAULT 'N',
  `code_page` varchar(10) NOT NULL DEFAULT 'TW',
  `department_uuid` varchar(36) DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  `site_uuid` varchar(36) DEFAULT NULL,
  `gender` varchar(1) DEFAULT 'M',
  `birthday` timestamp NULL DEFAULT NULL,
  `hire_date` timestamp NULL DEFAULT NULL,
  `quit_date` timestamp NULL DEFAULT NULL,
  `is_manager` varchar(1) DEFAULT 'N',
  `is_direct` varchar(1) DEFAULT 'N',
  `grade` varchar(10) DEFAULT NULL,
  `id` varchar(100) DEFAULT NULL,
  `src_uuid` varchar(36) DEFAULT NULL,
  `is_default_pass` varchar(1) DEFAULT 'Y',
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `ATTENDANT_UK1` (`company_uuid`,`account`)
  
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `application_head` (
  `uuid` varchar(36) NOT NULL,
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `name` varchar(200) NOT NULL,
  `description` varchar(500) DEFAULT NULL,
  `id` varchar(100) NOT NULL,
  `create_user` varchar(36) DEFAULT NULL,
  `update_user` varchar(36) DEFAULT NULL,
  `web_site` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `APPLICATION_HEAD_UK1` (`name`),
  KEY `APPLICATION_HEAD_UK2` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `group_head` (
  `uuid` varchar(36) NOT NULL,
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `name_zh_tw` varchar(200) NOT NULL,
  `name_zh_cn` varchar(200) NOT NULL,
  `name_en_us` varchar(200) NOT NULL,
  `company_uuid` varchar(36) NOT NULL,
  `id` varchar(100) NOT NULL,
  `create_user` varchar(36) NOT NULL,
  `update_user` varchar(36) DEFAULT NULL,
  `application_head_uuid` varchar(36) NOT NULL,
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `GROUP_UK1` (`id`,`company_uuid`,`application_head_uuid`),
  KEY `GROUP_HEAD_APPLICATION_HE_FK1_idx` (`application_head_uuid`),
  KEY `GROUP_HEAD_COMPANY_FK1_idx` (`company_uuid`)  
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `group_attendant` (
  `uuid` varchar(36) NOT NULL,
  `is_active` varchar(1) NOT NULL,
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `create_user` varchar(36) NOT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_user` varchar(36) DEFAULT NULL,
  `group_head_uuid` varchar(36) NOT NULL,
  `attendant_uuid` varchar(36) NOT NULL,
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `GROUP_ATTENDANT_UK1` (`group_head_uuid`,`attendant_uuid`),
  KEY `GROUP_ATTENDANT_ATTENDANT_FK1_idx` (`attendant_uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `sitemap` (
  `uuid` varchar(36) NOT NULL,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `create_user` varchar(36) NOT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_user` varchar(36) DEFAULT NULL,
  `sitemap_uuid` varchar(36) DEFAULT NULL,
  `apppage_uuid` varchar(36) NOT NULL,
  `root_uuid` varchar(36) NOT NULL,
  `haschild` varchar(1) NOT NULL DEFAULT 'N',
  `application_head_uuid` varchar(36) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `SITEMAP_UK1` (`sitemap_uuid`,`apppage_uuid`),
  KEY `SITEMAP_APPLICATION_HEAD_FK1_idx` (`application_head_uuid`),
  KEY `SITEMAP_APPPAGE_FK1_idx` (`apppage_uuid`),
  KEY `SITEMAP_SITEMAP_FK2_idx` (`root_uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



CREATE TABLE `appmenu` (
  `uuid` varchar(36) NOT NULL,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `create_user` varchar(36) DEFAULT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_user` varchar(36) DEFAULT NULL,
  `name_zh_tw` varchar(200) NOT NULL,
  `name_zh_cn` varchar(200) NOT NULL,
  `name_en_us` varchar(200) NOT NULL,
  `id` varchar(100) NOT NULL,
  `appmenu_uuid` varchar(36) DEFAULT NULL,
  `haschild` varchar(1) NOT NULL,
  `application_head_uuid` varchar(36) NOT NULL,
  `ord` int(11) NOT NULL,
  `parameter_class` varchar(200) DEFAULT NULL,
  `image` varchar(100) DEFAULT NULL,
  `sitemap_uuid` varchar(36) DEFAULT NULL,
  `action_mode` varchar(50) DEFAULT NULL,
  `is_default_page` varchar(1) DEFAULT NULL,
  `is_admin` varchar(1) DEFAULT 'N',
  `appmenucol` varchar(45) DEFAULT 'N',
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `APPMENU_UK1` (`id`,`appmenu_uuid`,`action_mode`),
  KEY `APPMENU_APPLICATION_HEAD_FK1_idx` (`application_head_uuid`),
  KEY `APPMENU_APPMENU_FK1_idx` (`appmenu_uuid`),
  KEY `APPMENU_SITEMAP_FK1_idx` (`sitemap_uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `group_appmenu` (
  `uuid` varchar(36) NOT NULL,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `create_user` varchar(36) NOT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_user` varchar(36) DEFAULT NULL,
  `appmenu_uuid` varchar(36) NOT NULL,
  `group_head_uuid` varchar(36) NOT NULL,
  `is_default_page` varchar(1) NOT NULL DEFAULT 'N',
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `GROUP_APPMENU_UK1` (`appmenu_uuid`,`group_head_uuid`),
  KEY `GROUP_APPMENU_GROUP_HEAD_FK1_idx` (`group_head_uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `apppage` (
  `uuid` varchar(36) NOT NULL,
  `is_active` varchar(1) NOT NULL DEFAULT 'Y',
  `create_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `create_user` varchar(36) NOT NULL,
  `update_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `update_user` varchar(36) DEFAULT NULL,
  `id` varchar(100) NOT NULL,
  `name` varchar(200) NOT NULL,
  `description` varchar(500) DEFAULT NULL,
  `url` varchar(200) NOT NULL,
  `parameter_class` varchar(200) DEFAULT NULL,
  `application_head_uuid` varchar(36) NOT NULL,
  `p_mode` varchar(100) DEFAULT NULL,
  `runjsfunction` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `APPPAGE_UK2` (`application_head_uuid`,`url`,`p_mode`,`parameter_class`)  
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


ALTER TABLE `department` 
ADD CONSTRAINT `DEPARTMENT_FK3`
  FOREIGN KEY (`manager_uuid`)
  REFERENCES `attendant` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `department` 
ADD CONSTRAINT `DEPARTMENT_FK1`
  FOREIGN KEY (`company_uuid`)
  REFERENCES `company` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `site` 
ADD CONSTRAINT `SITE_FK1`
  FOREIGN KEY (`company_uuid`)
  REFERENCES `company` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `site` 
ADD CONSTRAINT `SITE_FK2`
  FOREIGN KEY (`parent_site_uuid`)
  REFERENCES `site` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;   
  
ALTER TABLE `attendant` 
ADD CONSTRAINT `ATTENDANT_FK1`
  FOREIGN KEY (`company_uuid`)
  REFERENCES `company` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `attendant` 
ADD CONSTRAINT `ATTENDANT_FK2`
  FOREIGN KEY (`department_uuid`)
  REFERENCES `department` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `attendant` 
ADD CONSTRAINT `ATTENDANT_FK3`
  FOREIGN KEY (`site_uuid`)
  REFERENCES `site` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  

ALTER TABLE `group_head` 
ADD CONSTRAINT `GROUP_HEAD_COMPANY_FK1`
  FOREIGN KEY (`company_uuid`)
  REFERENCES `company` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `group_head` 
ADD CONSTRAINT `GROUP_HEAD_APPLICATION_HE_FK1`
  FOREIGN KEY (`application_head_uuid`)
  REFERENCES `application_head` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `group_attendant` 
ADD CONSTRAINT `GROUP_ATTENDANT_ATTENDANT_FK1`
  FOREIGN KEY (`attendant_uuid`)
  REFERENCES `attendant` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  

ALTER TABLE `group_attendant` 
ADD CONSTRAINT `GROUP_ATTENDANT_GROUP_HEA_FK1`
  FOREIGN KEY (`group_head_uuid`)
  REFERENCES `group_head` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `sitemap` 
ADD CONSTRAINT `SITEMAP_APPLICATION_HEAD_FK1`
  FOREIGN KEY (`application_head_uuid`)
  REFERENCES `application_head` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `sitemap` 
ADD CONSTRAINT `SITEMAP_APPPAGE_FK1`
  FOREIGN KEY (`apppage_uuid`)
  REFERENCES `apppage` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `sitemap` 
ADD CONSTRAINT `SITEMAP_SITEMAP_FK1`
  FOREIGN KEY (`sitemap_uuid`)
  REFERENCES `sitemap` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `sitemap` 
ADD CONSTRAINT `SITEMAP_SITEMAP_FK2`
  FOREIGN KEY (`root_uuid`)
  REFERENCES `sitemap` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `appmenu` 
ADD CONSTRAINT `APPMENU_SITEMAP_FK1`
  FOREIGN KEY (`sitemap_uuid`)
  REFERENCES `sitemap` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `appmenu` 
ADD CONSTRAINT `APPMENU_APPLICATION_HEAD_FK1`
  FOREIGN KEY (`application_head_uuid`)
  REFERENCES `application_head` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `appmenu` 
ADD CONSTRAINT `APPMENU_APPMENU_FK1`
  FOREIGN KEY (`appmenu_uuid`)
  REFERENCES `appmenu` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `group_appmenu` 
ADD CONSTRAINT `GROUP_APPMENU_APPMENU_FK1`
  FOREIGN KEY (`appmenu_uuid`)
  REFERENCES `appmenu` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
ALTER TABLE `group_appmenu` 
ADD CONSTRAINT `GROUP_APPMENU_GROUP_HEAD_FK1`
  FOREIGN KEY (`group_head_uuid`)
  REFERENCES `group_head` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;  

ALTER TABLE `apppage` 
ADD CONSTRAINT `APPPAGE_APPLICATION_HEAD_FK1`
  FOREIGN KEY (`application_head_uuid`)
  REFERENCES `application_head` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  

CREATE TABLE `proxy` (
  `uuid` VARCHAR(36) NOT NULL,
  `Proxy_Action` VARCHAR(400) NULL,
  `Proxy_Method` VARCHAR(400) NULL,
  `description` VARCHAR(400) NULL,
  `Proxy_Type` VARCHAR(50) NULL,
  `need_redirect` VARCHAR(1) NULL DEFAULT 'N',
  `redirect_Proxy_Action` VARCHAR(400) NULL,
  `redirect_Proxy_Method` VARCHAR(400) NULL,
  `application_head_uuid` VARCHAR(36) NULL,
  `redirect_src` VARCHAR(400) NULL,
  PRIMARY KEY (`uuid`));
  
ALTER TABLE `proxy` 
ADD INDEX `FK_Proxy_APPLICATION_HEAD_idx` (`application_head_uuid` ASC);
ALTER TABLE `proxy` 
ADD CONSTRAINT `FK_Proxy_APPLICATION_HEAD`
  FOREIGN KEY (`application_head_uuid`)
  REFERENCES `application_head` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

  
CREATE TABLE `appmenu_proxy_map` (
  `uuid` VARCHAR(36) NOT NULL,
  `proxy_uuid` VARCHAR(36) NULL,
  `appMenu_uuid` VARCHAR(36) NULL,
  PRIMARY KEY (`uuid`),
  INDEX `FK_APPPAGE_PROXY_MAP_Proxy_idx` (`proxy_uuid` ASC),
  CONSTRAINT `FK_APPPAGE_PROXY_MAP_Proxy`
    FOREIGN KEY (`proxy_uuid`)
    REFERENCES `proxy` (`uuid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
	
	

CREATE TABLE `SCHEDULE`(
	`UUID` varchar(36) NOT NULL,
	`SCHEDULE_NAME` varchar(500) NOT NULL,	
	`SCHEDULE_END_DATE` timestamp NULL  ,
	`LAST_RUN_TIME` timestamp NULL,
	`LAST_RUN_STATUS` varchar(10) NULL,
	`IS_CYCLE` varchar(1) NOT NULL,
	`SINGLE_DATE` timestamp NULL,
	`HOUR` varchar(2) NULL,
	`MINUTE` varchar(2) NULL,
	`CYCLE_TYPE` varchar(100) NOT NULL,
	`C_MINUTE` int NULL,
	`C_HOUR` int NULL,
	`C_DAY` int NULL,
	`C_WEEK` int NULL,
	`C_DAY_OF_WEEK` varchar(50) NULL,
	`C_MONTH` int NULL,
	`C_DAY_OF_MONTH` varchar(50) NULL,
	`C_WEEK_OF_MONTH` varchar(50) NULL,
	`C_YEAR` int NULL,
	`C_WEEK_OF_YEAR` varchar(1024) NULL,
	`RUN_URL` varchar(1024) NOT NULL,
	`RUN_URL_PARAMETER` varchar(1024) NULL,
	`RUN_ATTENDANT_UUID` varchar(36) NULL,
	`IS_ACTIVE` varchar(1) NOT NULL,
	`START_DATE` timestamp NULL,
	`RUN_SECURITY` varchar(200) NULL,
	`EXPEND_ALL` varchar(1) NULL,
	`contiune_dateTime` timestamp NULL,	
	PRIMARY KEY (`uuid`)
);


CREATE TABLE SCHEDULE_TIME(
	UUID varchar(36) NOT NULL,
	SCHEDULE_UUID varchar(36) NOT NULL,
	START_TIME timestamp NOT NULL,
	FINISH_TIME timestamp NULL,
	STATUS varchar(50) NULL,
	PRIMARY KEY (`uuid`)
);


ALTER TABLE `schedule_time` 
ADD INDEX `FK_SCHEDULE_TIME_SCHEDULE_idx` (`SCHEDULE_UUID` ASC);
ALTER TABLE `schedule_time` 
ADD CONSTRAINT `FK_SCHEDULE_TIME_SCHEDULE`
  FOREIGN KEY (`SCHEDULE_UUID`)
  REFERENCES `schedule` (`UUID`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  

ALTER TABLE `schedule` 
ADD INDEX `FK_SCHEDULE_ATTENDANT_idx` (`RUN_ATTENDANT_UUID` ASC);
ALTER TABLE `schedule` 
ADD CONSTRAINT `FK_SCHEDULE_ATTENDANT`
  FOREIGN KEY (`RUN_ATTENDANT_UUID`)
  REFERENCES `attendant` (`uuid`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
