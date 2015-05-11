create table {user}.COMPANY
(
  uuid                 VARCHAR2(36) not null,
  create_date          DATE default sysdate,
  update_date          DATE default sysdate,
  is_active            VARCHAR2(1) default 'Y' not null,
  class                VARCHAR2(10) not null,
  id                   VARCHAR2(100) not null,
  c_name               VARCHAR2(100),
  e_name               VARCHAR2(100),
  voucher_point_uuid   VARCHAR2(36),
  week_shift           NUMBER default 0,
  ou_sync_type         VARCHAR2(1) default 'F',
  name_zh_cn           VARCHAR2(128),
  concurrent_user      VARCHAR2(128),
  expired_date         DATE,
  sales_attendant_uuid VARCHAR2(36),
  is_sync_ad_user varchar2(1) default 'N',
  ad_ldap varchar2(400) default '' ,
ad_ldap_user varchar2(400) default '' ,
ad_ldap_user_password varchar2(50) default ''
);
alter table {user}.COMPANY
  add constraint COMPANY_PK primary key (UUID)
  using index ;

alter table {user}.COMPANY
  add constraint COMPANY_UK1 unique (ID)
  using index ; 

CREATE TABLE {user}.ACTIVE_CONNECTION(
	UUID varchar2(36) NOT NULL,
	ACCOUNT varchar2(40) NOT NULL,
	COMPANY_UUID varchar2(36) NOT NULL,
	IP varchar2(20) NOT NULL,
	APPLICATION varchar2(100) NOT NULL,
	STARTTIME DATE default sysdate NOT NULL,	
	EXPIRESTIME DATE  default sysdate NOT NULL,
	STATUS varchar2(10) NULL
) ;
alter table {user}.ACTIVE_CONNECTION
  add constraint ACTIVE_CONNECTION_PK primary key (UUID)
  using index ;



  
create table {user}.DEPARTMENT
(
  uuid                        VARCHAR2(36) not null,
  create_date                 DATE default sysdate,
  update_date                 DATE default sysdate,
  is_active                   VARCHAR2(1) default 'Y' not null,
  company_uuid                VARCHAR2(36) not null,
  id                          VARCHAR2(100) not null,
  c_name                      VARCHAR2(100),
  e_name                      VARCHAR2(100),
  parent_department_uuid      VARCHAR2(36),
  manager_uuid                VARCHAR2(36),
  parent_department_id        VARCHAR2(100),
  manager_id                  VARCHAR2(100),
  parent_department_uuid_list VARCHAR2(4000),
  s_name                      VARCHAR2(100),
  cost_center                 VARCHAR2(100),
  src_uuid                    VARCHAR2(36),
  full_department_name        VARCHAR2(4000)
);
create index {user}.DEPARTMENT_IDX1 on {user}.DEPARTMENT (PARENT_DEPARTMENT_UUID);
alter table {user}.DEPARTMENT
  add constraint DEPARTMENT_PK primary key (UUID)
  using index 
  ;
alter table {user}.DEPARTMENT
  add constraint DEPARTMENT_UK1 unique (COMPANY_UUID, ID)
  using index ;
alter table {user}.DEPARTMENT
  add constraint DEPARTMENT_FK1 foreign key (COMPANY_UUID)
  references {user}.COMPANY (UUID);
alter table {user}.DEPARTMENT
  add constraint DEPARTMENT_FK2 foreign key (PARENT_DEPARTMENT_UUID)
  references {user}.DEPARTMENT (UUID);


create table {user}.SITE
(
  uuid             VARCHAR2(36) not null,
  create_date      DATE default sysdate,
  update_date      DATE default sysdate,
  is_active        VARCHAR2(1) default 'Y' not null,
  company_uuid     VARCHAR2(36) not null,
  id               VARCHAR2(100) not null,
  c_name           VARCHAR2(100),
  e_name           VARCHAR2(100),
  parent_site_uuid VARCHAR2(36)
);
alter table {user}.SITE
  add constraint SITE_PK primary key (UUID)
  using index ;
alter table {user}.SITE
  add constraint SITE_UK1 unique (COMPANY_UUID, ID)
  using index ;
alter table {user}.SITE
  add constraint SITE_FK1 foreign key (COMPANY_UUID)
  references {user}.COMPANY (UUID);
alter table {user}.SITE
  add constraint SITE_FK2 foreign key (PARENT_SITE_UUID)
  references {user}.SITE (UUID);


create table {user}.ATTENDANT
(
  uuid            VARCHAR2(36) not null,
  create_date     DATE default sysdate,
  update_date     DATE default sysdate,
  is_active       VARCHAR2(1) default 'Y' not null,
  company_uuid    VARCHAR2(36) not null,
  account         VARCHAR2(50) not null,
  c_name          VARCHAR2(100),
  e_name          VARCHAR2(100),
  email           VARCHAR2(300) not null,
  password        VARCHAR2(50),
  is_supper       VARCHAR2(1) default 'N' not null,
  is_admin        VARCHAR2(1) default 'N' not null,
  code_page       VARCHAR2(10) default 'TW' not null,
  department_uuid VARCHAR2(36),
  phone           VARCHAR2(50),
  site_uuid       VARCHAR2(36),
  gender          VARCHAR2(1) default 'M',
  birthday        DATE,
  hire_date       DATE,
  quit_date       DATE,
  is_manager      VARCHAR2(1) default 'N',
  is_direct       VARCHAR2(1) default 'N',
  grade           VARCHAR2(10),
  id              VARCHAR2(100),
  src_uuid        VARCHAR2(36),
  is_default_pass VARCHAR2(1) default 'Y',
  picture_url     varchar2(200) default '~/css/images/nopicture.jpg'
);
create index {user}.ATTENDANT_IDX1 on {user}.ATTENDANT (COMPANY_UUID, ID);
create index {user}.ATTENDANT_IDX2 on {user}.ATTENDANT (SRC_UUID);
alter table {user}.ATTENDANT
  add constraint ATTENDANT_PK primary key (UUID);
alter table {user}.ATTENDANT
  add constraint ATTENDANT_UK1 unique (COMPANY_UUID, ACCOUNT)
  using index ;
alter table {user}.ATTENDANT
  add constraint ATTENDANT_FK1 foreign key (COMPANY_UUID)
  references {user}.COMPANY (UUID);
alter table {user}.ATTENDANT
  add constraint ATTENDANT_FK2 foreign key (DEPARTMENT_UUID)
  references {user}.DEPARTMENT (UUID);
alter table {user}.ATTENDANT
  add constraint ATTENDANT_FK3 foreign key (SITE_UUID)
  references {user}.SITE (UUID)
  disable;

alter table {user}.DEPARTMENT
  add constraint DEPARTMENT_FK3 foreign key (MANAGER_UUID)
  references {user}.ATTENDANT (UUID);
  
create table {user}.APPLICATION_HEAD
(
  uuid        VARCHAR2(36) not null,
  create_date DATE default sysdate not null,
  update_date DATE default sysdate,
  is_active   VARCHAR2(1) default 'Y' not null,
  name        VARCHAR2(100) not null,
  description VARCHAR2(500),
  id          VARCHAR2(100) default '1' not null,
  web_site    VARCHAR2(500),
  create_user VARCHAR2(36),
  update_user VARCHAR2(36)
);
alter table {user}.APPLICATION_HEAD
  add constraint APPLICATION_HEAD_PK primary key (UUID)
  using index ;
alter table {user}.APPLICATION_HEAD
  add constraint APPLICATION_HEAD_UK1 unique (NAME)
  using index ;
alter table {user}.APPLICATION_HEAD
  add constraint APPLICATION_HEAD_UK2 unique (ID)
  using index ;

create table {user}.GROUP_HEAD
(
  uuid                  VARCHAR2(36) not null,
  create_date           DATE default sysdate not null,
  update_date           DATE default sysdate,
  is_active             VARCHAR2(1) default 'Y' not null,
  name_zh_tw            VARCHAR2(200) not null,
  name_zh_cn            VARCHAR2(200) not null,
  name_en_us            VARCHAR2(200) not null,
  company_uuid          VARCHAR2(36) not null,
  id                    VARCHAR2(100) not null,
  create_user           VARCHAR2(36) not null,
  update_user           VARCHAR2(36),
  application_head_uuid VARCHAR2(36) not null
);
alter table {user}.GROUP_HEAD
  add constraint GROUP_PK primary key (UUID)
  using index ;
alter table {user}.GROUP_HEAD
  add constraint GROUP_HEAD_UK1 unique (ID, COMPANY_UUID, APPLICATION_HEAD_UUID)
  using index ;
alter table {user}.GROUP_HEAD
  add constraint GROUP_HEAD_APPLICATION_HE_FK1 foreign key (APPLICATION_HEAD_UUID)
  references {user}.APPLICATION_HEAD (UUID);
alter table {user}.GROUP_HEAD
  add constraint GROUP_HEAD_COMPANY_FK1 foreign key (COMPANY_UUID)
  references {user}.COMPANY (UUID); 
  
create table {user}.GROUP_ATTENDANT
(
  uuid            VARCHAR2(36) not null,
  is_active       VARCHAR2(1) not null,
  create_date     DATE default SYSDATE not null,
  create_user     VARCHAR2(36) not null,
  update_date     DATE default SYSDATE,
  update_user     VARCHAR2(36),
  group_head_uuid VARCHAR2(36) not null,
  attendant_uuid  VARCHAR2(36) not null
);
alter table {user}.GROUP_ATTENDANT
  add constraint GROUP_ATTENDANT_PK primary key (UUID)
  using index ;
alter table {user}.GROUP_ATTENDANT
  add constraint GROUP_ATTENDANT_UK1 unique (GROUP_HEAD_UUID, ATTENDANT_UUID)
  using index ;
alter table {user}.GROUP_ATTENDANT
  add constraint GROUP_ATTENDANT_ATTENDANT_FK1 foreign key (ATTENDANT_UUID)
  references {user}.ATTENDANT (UUID);
alter table {user}.GROUP_ATTENDANT
  add constraint GROUP_ATTENDANT_GROUP_HEA_FK1 foreign key (GROUP_HEAD_UUID)
  references {user}.GROUP_HEAD (UUID);  

create table {user}.SITEMAP
(
  uuid                  VARCHAR2(36) not null,
  is_active             VARCHAR2(1) default 'Y' not null,
  create_date           DATE default SYSDATE not null,
  create_user           VARCHAR2(36) not null,
  update_date           DATE default SYSDATE,
  update_user           VARCHAR2(36),
  sitemap_uuid          VARCHAR2(36),
  apppage_uuid          VARCHAR2(36) not null,
  root_uuid             VARCHAR2(36) not null,
  haschild              VARCHAR2(1) default 'N' not null,
  application_head_uuid VARCHAR2(36)
);
alter table {user}.SITEMAP
  add constraint SITEMAP_PK primary key (UUID)
  using index ;
alter table {user}.SITEMAP
  add constraint SITEMAP_UK1 unique (APPPAGE_UUID, SITEMAP_UUID)
  using index ;
alter table {user}.SITEMAP
  add constraint SITEMAP_APPLICATION_HEAD_FK1 foreign key (APPLICATION_HEAD_UUID)
  references {user}.APPLICATION_HEAD (UUID);

alter table {user}.SITEMAP
  add constraint SITEMAP_SITEMAP_FK1 foreign key (SITEMAP_UUID)
  references {user}.SITEMAP (UUID);
alter table {user}.SITEMAP
  add constraint SITEMAP_SITEMAP_FK2 foreign key (ROOT_UUID)
  references {user}.SITEMAP (UUID);
  
create table {user}.APPMENU
(
  uuid                  VARCHAR2(36) not null,
  is_active             VARCHAR2(1) default 'Y' not null,
  create_date           DATE default SYSDATE not null,
  create_user           VARCHAR2(36) not null,
  update_date           DATE default SYSDATE,
  update_user           VARCHAR2(36),
  name_zh_tw            VARCHAR2(200) not null,
  name_zh_cn            VARCHAR2(200) not null,
  name_en_us            VARCHAR2(200) not null,
  name_jpn              VARCHAR2(200) null,
  id                    VARCHAR2(100) not null,
  appmenu_uuid          VARCHAR2(36),
  haschild              VARCHAR2(1) default 'N' not null,
  application_head_uuid VARCHAR2(36) not null,
  ord                   NUMBER not null,
  parameter_class       VARCHAR2(200),
  image                 VARCHAR2(100),
  sitemap_uuid          VARCHAR2(36),
  action_mode           VARCHAR2(50),
  is_default_page       VARCHAR2(1) default 'N',
  is_admin              VARCHAR2(1) default 'N'
);
alter table {user}.APPMENU
  add constraint APPMENU_PK primary key (UUID)
  using index ;
alter table {user}.APPMENU
  add constraint APPMENU_UK1 unique (APPLICATION_HEAD_UUID, ID)
  using index ;
alter table {user}.APPMENU
  add constraint APPMENU_UK2 unique (APPLICATION_HEAD_UUID, ID, ACTION_MODE)
  using index ;
alter table {user}.APPMENU
  add constraint APPMENU_APPLICATION_HEAD_FK1 foreign key (APPLICATION_HEAD_UUID)
  references {user}.APPLICATION_HEAD (UUID);
alter table {user}.APPMENU
  add constraint APPMENU_APPMENU_FK1 foreign key (APPMENU_UUID)
  references {user}.APPMENU (UUID);
alter table {user}.APPMENU
  add constraint APPMENU_SITEMAP_FK1 foreign key (SITEMAP_UUID)
  references {user}.SITEMAP (UUID);

create table {user}.GROUP_APPMENU
(
  uuid            VARCHAR2(36) not null,
  is_active       VARCHAR2(1) default 'Y' not null,
  create_date     DATE default SYSDATE not null,
  create_user     VARCHAR2(36) not null,
  update_date     DATE default SYSDATE,
  update_user     VARCHAR2(36),
  appmenu_uuid    VARCHAR2(36) not null,
  group_head_uuid VARCHAR2(36) not null,
  is_default_page VARCHAR2(1) default 'N' not null
);
alter table {user}.GROUP_APPMENU
  add constraint GROUP_APPMENU_PK primary key (UUID)
  using index ;
alter table {user}.GROUP_APPMENU
  add constraint GROUP_APPMENU_UK1 unique (GROUP_HEAD_UUID, APPMENU_UUID)
  using index ;
alter table {user}.GROUP_APPMENU
  add constraint GROUP_APPMENU_APPMENU_FK1 foreign key (APPMENU_UUID)
  references {user}.APPMENU (UUID);
alter table {user}.GROUP_APPMENU
  add constraint GROUP_APPMENU_GROUP_HEAD_FK1 foreign key (GROUP_HEAD_UUID)
  references {user}.GROUP_HEAD (UUID);
  
create table {user}.APPPAGE
(
  uuid                  VARCHAR2(36) not null,
  is_active             VARCHAR2(1) default 'Y' not null,
  create_date           DATE default SYSDATE not null,
  create_user           VARCHAR2(36) not null,
  update_date           DATE default SYSDATE,
  update_user           VARCHAR2(36),
  id                    VARCHAR2(100) not null,
  name                  VARCHAR2(200) not null,
  description           VARCHAR2(500),
  url                   VARCHAR2(200) not null,
  parameter_class       VARCHAR2(200),
  application_head_uuid VARCHAR2(36) not null,
  runjsfunction         VARCHAR2(200) ,
  p_mode                VARCHAR2(100)
);
alter table {user}.APPPAGE
  add constraint APPPAGE_PK primary key (UUID)
  using index ;
alter table {user}.APPPAGE
  add constraint APPPAGE_UK1 unique (APPLICATION_HEAD_UUID, ID)
  using index ;
alter table {user}.APPPAGE
  add constraint APPPAGE_UK2 unique (APPLICATION_HEAD_UUID, NAME, URL, P_MODE, PARAMETER_CLASS)
  using index ;
alter table {user}.APPPAGE
  add constraint APPPAGE_APPLICATION_HEAD_FK1 foreign key (APPLICATION_HEAD_UUID)
  references {user}.APPLICATION_HEAD (UUID);
alter table {user}.SITEMAP
  add constraint SITEMAP_APPPAGE_FK1 foreign key (APPPAGE_UUID)
  references {user}.APPPAGE (UUID);
  
  
  

create table {user}.Proxy(
	UUID VARCHAR2(36) NOT NULL,
	Proxy_Action VARCHAR2(400) NOT NULL,
	Proxy_Method VARCHAR2(400) NOT NULL,
	description VARCHAR2(400) NOT NULL,
	Proxy_Type VARCHAR2(50) NOT NULL,
	need_redirect VARCHAR2(1) default 'N' NOT NULL,
	redirect_Proxy_Action VARCHAR2(400) NULL,
	redirect_Proxy_Method VARCHAR2(400) NULL,
	application_head_uuid VARCHAR2(36) NOT NULL,
	redirect_src VARCHAR2(400) NULL
);

alter table {user}.Proxy
  add constraint PK_Proxy primary key (UUID)
  using index ;  


create table {user}.APPMENU_PROXY_MAP(
	uuid VARCHAR2(36) NOT NULL,
	proxy_uuid VARCHAR2(36) NOT NULL,
	appMenu_uuid VARCHAR2(36) NOT NULL);
	
alter table {user}.APPMENU_PROXY_MAP
  add constraint PK_APPPAGE_PROXY_MAP_1 primary key (UUID)
  using index ;  
  

alter table {user}.Proxy
  add constraint FK_Proxy_APPLICATION_HEAD foreign key (APPLICATION_HEAD_UUID)
  references {user}.APPLICATION_HEAD (UUID);

alter table {user}.APPMENU_PROXY_MAP
  add constraint FK_APPPAGE_PROXY_MAP_Proxy foreign key (proxy_uuid)
  references {user}.Proxy (UUID);
  
  


CREATE TABLE {user}.SCHEDULE(
	UUID VARCHAR2(36) NOT NULL,
	SCHEDULE_NAME VARCHAR2(500) NOT NULL,
	SCHEDULE_END_DATE DATE NOT NULL,	
	LAST_RUN_TIME DATE NULL,
	LAST_RUN_STATUS VARCHAR2(10) NULL,
	IS_CYCLE VARCHAR2(1) NOT NULL,
	SINGLE_DATE DATE NULL,
	HOUR VARCHAR2(2) NULL,
	MINUTE VARCHAR2(2) NULL,
	CYCLE_TYPE VARCHAR2(100) NOT NULL,
	C_MINUTE int NULL,
	C_HOUR int NULL,
	C_DAY int NULL,
	C_WEEK int NULL,
	C_DAY_OF_WEEK VARCHAR2(50) NULL,
	C_MONTH int NULL,
	C_DAY_OF_MONTH VARCHAR2(50) NULL,
	C_WEEK_OF_MONTH VARCHAR2(50) NULL,
	C_YEAR int NULL,
	C_WEEK_OF_YEAR VARCHAR2(1024) NULL,
	RUN_URL VARCHAR2(1024) NOT NULL,
	RUN_URL_PARAMETER VARCHAR2(1024) NULL,
	RUN_ATTENDANT_UUID VARCHAR2(36) NULL,
	IS_ACTIVE VARCHAR2(1) NOT NULL,
	START_DATE DATE NULL,
	RUN_SECURITY VARCHAR2(200) NULL,
	EXPEND_ALL VARCHAR2(1) NULL,
	contiune_dateTime DATE NULL);
	
alter table {user}.SCHEDULE
  add constraint PK_SCHEDULE primary key (UUID)
  using index ; 

CREATE TABLE {user}.SCHEDULE_TIME(
	UUID VARCHAR2(36) NOT NULL,
	SCHEDULE_UUID VARCHAR2(36) NOT NULL,
	START_TIME DATE NOT NULL,
	FINISH_TIME DATE NULL,
	STATUS VARCHAR2(50) NULL
	);
	
alter table {user}.SCHEDULE_TIME
  add constraint PK_SCHEDULE_TIME primary key (UUID)
  using index ;   

alter table {user}.SCHEDULE
  add constraint FK_SCHEDULE_ATTENDANT foreign key (RUN_ATTENDANT_UUID)
  references {user}.attendant (UUID);
  
alter table {user}.SCHEDULE_TIME
  add constraint FK_SCHEDULE_TIME_SCHEDULE foreign key (SCHEDULE_UUID)
  references schedule (UUID);
	
  
