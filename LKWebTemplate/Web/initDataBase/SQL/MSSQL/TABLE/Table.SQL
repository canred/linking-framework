CREATE TABLE [dbo].[COMPANY](
	[uuid] [varchar](36) NOT NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
	[is_active] [varchar](1) NOT NULL,
	[class] [nvarchar](50) NOT NULL,
	[id] [nvarchar](100) NOT NULL,
	[c_name] [nvarchar](100) NULL,
	[e_name] [nvarchar](100) NULL,
	[voucher_point_uuid] [varchar](36) NULL,
	[week_shift] [numeric](18, 0) NULL,
	[ou_sync_type] [varchar](1) NULL,
	[name_zh_cn] [nvarchar](128) NULL,
	[concurrent_user] [nvarchar](128) NULL,
	[expired_date] [datetime] NULL,
	[sales_attendant_uuid] [varchar](36) NULL,
	[is_sync_ad_user] [varchar](1) NULL,
	[ad_ldap] [varchar](400) NULL,
	[ad_ldap_user] [varchar](400) NULL,
	[ad_ldap_user_password] [varchar](50) NULL,
 CONSTRAINT [COMPANY_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [COMPANY_UK1] UNIQUE NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[ACTIVE_CONNECTION](
	[UUID] [varchar](36) NOT NULL,
	[ACCOUNT] [varchar](40) NOT NULL,
	[COMPANY_UUID] [varchar](36) NOT NULL,
	[IP] [varchar](20) NOT NULL,
	[APPLICATION] [varchar](100) NOT NULL,
	[STARTTIME] [date] NOT NULL,
	[EXPIRESTIME] [date] NOT NULL,
	[STATUS] [varchar](10) NULL,
 CONSTRAINT [PK_ACTIVE_CONNECTION] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[DEPARTMENT](
	[uuid] [varchar](36) NOT NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
	[is_active] [varchar](1) NOT NULL,
	[company_uuid] [varchar](36) NOT NULL,
	[id] [varchar](100) NOT NULL,
	[c_name] [nvarchar](100) NULL,
	[e_name] [varchar](100) NULL,
	[parent_department_uuid] [varchar](36) NULL,
	[manager_uuid] [varchar](36) NULL,
	[parent_department_id] [varchar](100) NULL,
	[manager_id] [varchar](100) NULL,
	[parent_department_uuid_list] [varchar](4000) NULL,
	[s_name] [nvarchar](100) NULL,
	[cost_center] [varchar](100) NULL,
	[src_uuid] [varchar](36) NULL,
	[full_department_name] [varchar](4000) NULL,
 CONSTRAINT [DEPARTMENT_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [DEPARTMENT_UK1] UNIQUE NONCLUSTERED 
(
	[company_uuid] ASC,
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[B_STRATEGE_HEAD](
	[UUID] [varchar](50) NULL,
	[CREATE_DATE] [datetime] NULL,
	[UPDATE_DATE] [datetime] NULL,
	[IS_ACTIVE] [varchar](50) NULL,
	[NAME] [varchar](50) NULL,
	[DESCRIPTION] [varchar](50) NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[SITE](
	[uuid] [varchar](36) NOT NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
	[is_active] [varchar](1) NOT NULL,
	[company_uuid] [varchar](36) NOT NULL,
	[id] [varchar](100) NOT NULL,
	[c_name] [nvarchar](100) NULL,
	[e_name] [varchar](100) NULL,
	[parent_site_uuid] [varchar](36) NULL,
 CONSTRAINT [SITE_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [SITE_UK1] UNIQUE NONCLUSTERED 
(
	[company_uuid] ASC,
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[ATTENDANT](
	[uuid] [varchar](36) NOT NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
	[is_active] [varchar](1) NOT NULL,
	[company_uuid] [varchar](36) NOT NULL,
	[account] [varchar](50) NOT NULL,
	[c_name] [nvarchar](100) NULL,
	[e_name] [varchar](100) NULL,
	[email] [varchar](300) NOT NULL,
	[password] [varchar](50) NULL,
	[is_supper] [varchar](1) NOT NULL,
	[is_admin] [varchar](1) NOT NULL,
	[code_page] [varchar](10) NOT NULL,
	[department_uuid] [varchar](36) NULL,
	[phone] [varchar](50) NULL,
	[site_uuid] [varchar](36) NULL,
	[gender] [varchar](1) NULL,
	[birthday] [datetime] NULL,
	[hire_date] [datetime] NULL,
	[quit_date] [datetime] NULL,
	[is_manager] [varchar](1) NULL,
	[is_direct] [varchar](1) NULL,
	[grade] [varchar](10) NULL,
	[id] [varchar](100) NULL,
	[src_uuid] [varchar](36) NULL,
	[is_default_pass] [varchar](1) NULL,
 CONSTRAINT [ATTENDANT_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [ATTENDANT_UK1] UNIQUE NONCLUSTERED 
(
	[company_uuid] ASC,
	[account] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[APPLICATION_HEAD](
	[uuid] [varchar](36) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_date] [datetime] NULL,
	[is_active] [varchar](1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[description] [nvarchar](500) NULL,
	[id] [nvarchar](100) NOT NULL,
	[create_user] [varchar](36) NULL,
	[update_user] [varchar](36) NULL,
	[web_site] [varchar](200) NULL,
 CONSTRAINT [APPLICATION_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [APPLICATION_HEAD_UK1] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [APPLICATION_HEAD_UK2] UNIQUE NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[GROUP_HEAD](
	[uuid] [varchar](36) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_date] [datetime] NULL,
	[is_active] [varchar](1) NOT NULL,
	[name_zh_tw] [varchar](200) NOT NULL,
	[name_zh_cn] [nvarchar](200) NOT NULL,
	[name_en_us] [varchar](200) NOT NULL,
	[company_uuid] [varchar](36) NOT NULL,
	[id] [varchar](100) NOT NULL,
	[create_user] [varchar](36) NOT NULL,
	[update_user] [varchar](36) NULL,
	[application_head_uuid] [varchar](36) NOT NULL,
 CONSTRAINT [GROUP_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [GROUP_UK1] UNIQUE NONCLUSTERED 
(
	[id] ASC,
	[company_uuid] ASC,
	[application_head_uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[GROUP_ATTENDANT](
	[uuid] [varchar](36) NOT NULL,
	[is_active] [varchar](1) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_user] [varchar](36) NOT NULL,
	[update_date] [datetime] NULL,
	[update_user] [varchar](36) NULL,
	[group_head_uuid] [varchar](36) NOT NULL,
	[attendant_uuid] [varchar](36) NOT NULL,
 CONSTRAINT [GROUP_ATTENDANT_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [GROUP_ATTENDANT_UK1] UNIQUE NONCLUSTERED 
(
	[group_head_uuid] ASC,
	[attendant_uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[SITEMAP](
	[uuid] [varchar](36) NOT NULL,
	[is_active] [varchar](1) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_user] [varchar](36) NOT NULL,
	[update_date] [datetime] NULL,
	[update_user] [varchar](36) NULL,
	[sitemap_uuid] [varchar](36) NULL,
	[apppage_uuid] [varchar](36) NOT NULL,
	[root_uuid] [varchar](36) NOT NULL,
	[haschild] [varchar](1) NOT NULL,
	[application_head_uuid] [varchar](36) NULL,
 CONSTRAINT [SITEMAP_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [SITEMAP_UK1] UNIQUE NONCLUSTERED 
(
	[sitemap_uuid] ASC,
	[apppage_uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];



CREATE TABLE [dbo].[APPMENU](
	[uuid] [varchar](36) NOT NULL,
	[is_active] [varchar](1) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_user] [varchar](36) NOT NULL,
	[update_date] [datetime] NULL,
	[update_user] [varchar](36) NULL,
	[name_zh_tw] [varchar](200) NOT NULL,
	[name_zh_cn] [nvarchar](200) NOT NULL,
	[name_en_us] [varchar](200) NOT NULL,
	[id] [varchar](100) NOT NULL,
	[appmenu_uuid] [varchar](36) NULL,
	[haschild] [varchar](1) NOT NULL,
	[application_head_uuid] [varchar](36) NOT NULL,
	[ord] [numeric](18, 0) NOT NULL,
	[parameter_class] [varchar](200) NULL,
	[image] [varchar](100) NULL,
	[sitemap_uuid] [varchar](36) NULL,
	[action_mode] [varchar](50) NULL,
	[is_default_page] [varchar](1) NULL,
	[is_admin] [varchar](1) NULL,
 CONSTRAINT [APPMENU_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [APPMENU_UK1] UNIQUE NONCLUSTERED 
(
	[application_head_uuid] ASC,
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [APPMENU_UK2] UNIQUE NONCLUSTERED 
(
	[application_head_uuid] ASC,
	[id] ASC,
	[action_mode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[GROUP_APPMENU](
	[uuid] [varchar](36) NOT NULL,
	[is_active] [varchar](1) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_user] [varchar](36) NOT NULL,
	[update_date] [datetime] NULL,
	[update_user] [varchar](36) NULL,
	[appmenu_uuid] [varchar](36) NOT NULL,
	[group_head_uuid] [varchar](36) NOT NULL,
	[is_default_page] [varchar](1) NOT NULL,
 CONSTRAINT [GROUP_APPMENU_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [GROUP_APPMENU_UK1] UNIQUE NONCLUSTERED 
(
	[appmenu_uuid] ASC,
	[group_head_uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[APPPAGE](
	[uuid] [varchar](36) NOT NULL,
	[is_active] [varchar](1) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_user] [varchar](36) NOT NULL,
	[update_date] [datetime] NULL,
	[update_user] [varchar](36) NULL,
	[id] [varchar](100) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[description] [nvarchar](500) NULL,
	[url] [varchar](200) NOT NULL,
	[parameter_class] [varchar](200) NULL,
	[application_head_uuid] [varchar](36) NOT NULL,
	[p_mode] [varchar](100) NULL,
 CONSTRAINT [APPPAGE_PK] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [APPPAGE_UK1] UNIQUE NONCLUSTERED 
(
	[id] ASC,
	[application_head_uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [APPPAGE_UK2] UNIQUE NONCLUSTERED 
(
	[application_head_uuid] ASC,
	[url] ASC,
	[p_mode] ASC,
	[parameter_class] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];





ALTER TABLE [dbo].[APPLICATION_HEAD] ADD  CONSTRAINT [DF__APPLICATI__creat__023D5A04]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[APPLICATION_HEAD] ADD  CONSTRAINT [DF__APPLICATI__updat__03317E3D]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[APPLICATION_HEAD] ADD  CONSTRAINT [DF__APPLICATI__is_ac__0425A276]  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[APPMENU] ADD  CONSTRAINT [DF__APPMENU__is_acti__08EA5793]  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[APPMENU] ADD  CONSTRAINT [DF__APPMENU__create___09DE7BCC]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[APPMENU] ADD  CONSTRAINT [DF__APPMENU__update___0AD2A005]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[APPMENU] ADD  CONSTRAINT [DF__APPMENU__haschil__0BC6C43E]  DEFAULT ('N') FOR [haschild];

ALTER TABLE [dbo].[APPMENU] ADD  CONSTRAINT [DF__APPMENU__is_defa__0CBAE877]  DEFAULT ('N') FOR [is_default_page];

ALTER TABLE [dbo].[APPMENU] ADD  CONSTRAINT [DF__APPMENU__is_admi__0DAF0CB0]  DEFAULT ('N') FOR [is_admin];

ALTER TABLE [dbo].[APPPAGE] ADD  CONSTRAINT [DF__APPPAGE__is_acti__286302EC]  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[APPPAGE] ADD  CONSTRAINT [DF__APPPAGE__create___29572725]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[APPPAGE] ADD  CONSTRAINT [DF__APPPAGE__update___2A4B4B5E]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__creat__38996AB5]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__updat__398D8EEE]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__is_ac__3A81B327]  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__is_su__3B75D760]  DEFAULT ('N') FOR [is_supper];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__is_ad__3C69FB99]  DEFAULT ('N') FOR [is_admin];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__code___3D5E1FD2]  DEFAULT ('TW') FOR [code_page];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__gende__3E52440B]  DEFAULT ('M') FOR [gender];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__is_ma__3F466844]  DEFAULT ('N') FOR [is_manager];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__is_di__403A8C7D]  DEFAULT ('N') FOR [is_direct];

ALTER TABLE [dbo].[ATTENDANT] ADD  CONSTRAINT [DF__ATTENDANT__is_de__412EB0B6]  DEFAULT ('Y') FOR [is_default_pass];

ALTER TABLE [dbo].[COMPANY] ADD  CONSTRAINT [DF__COMPANY__create___30F848ED]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[COMPANY] ADD  CONSTRAINT [DF__COMPANY__update___31EC6D26]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[COMPANY] ADD  CONSTRAINT [DF__COMPANY__is_acti__32E0915F]  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[COMPANY] ADD  CONSTRAINT [DF__COMPANY__week_sh__33D4B598]  DEFAULT ((0)) FOR [week_shift];

ALTER TABLE [dbo].[COMPANY] ADD  CONSTRAINT [DF__COMPANY__ou_sync__34C8D9D1]  DEFAULT ('F') FOR [ou_sync_type];

ALTER TABLE [dbo].[DEPARTMENT] ADD  CONSTRAINT [DF__DEPARTMEN__creat__47DBAE45]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[DEPARTMENT] ADD  CONSTRAINT [DF__DEPARTMEN__updat__48CFD27E]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[DEPARTMENT] ADD  CONSTRAINT [DF__DEPARTMEN__is_ac__49C3F6B7]  DEFAULT ('Y') FOR [is_active];



ALTER TABLE [dbo].[GROUP_APPMENU] ADD  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[GROUP_APPMENU] ADD  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[GROUP_APPMENU] ADD  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[GROUP_APPMENU] ADD  DEFAULT ('N') FOR [is_default_page];

ALTER TABLE [dbo].[GROUP_ATTENDANT] ADD  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[GROUP_ATTENDANT] ADD  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[GROUP_HEAD] ADD  CONSTRAINT [DF__GROUP_HEA__creat__5EBF139D]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[GROUP_HEAD] ADD  CONSTRAINT [DF__GROUP_HEA__updat__5FB337D6]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[GROUP_HEAD] ADD  CONSTRAINT [DF__GROUP_HEA__is_ac__60A75C0F]  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[SITE] ADD  CONSTRAINT [DF__SITE__create_dat__20C1E124]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[SITE] ADD  CONSTRAINT [DF__SITE__update_dat__21B6055D]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[SITE] ADD  CONSTRAINT [DF__SITE__is_active__22AA2996]  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[SITEMAP] ADD  DEFAULT ('Y') FOR [is_active];

ALTER TABLE [dbo].[SITEMAP] ADD  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[SITEMAP] ADD  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[SITEMAP] ADD  DEFAULT ('N') FOR [haschild];

ALTER TABLE [dbo].[APPMENU]  WITH CHECK ADD  CONSTRAINT [APPMENU_APPLICATION_HEAD_FK1] FOREIGN KEY([application_head_uuid])
REFERENCES [dbo].[APPLICATION_HEAD] ([uuid]);

ALTER TABLE [dbo].[APPMENU] CHECK CONSTRAINT [APPMENU_APPLICATION_HEAD_FK1];

ALTER TABLE [dbo].[APPMENU]  WITH CHECK ADD  CONSTRAINT [APPMENU_APPMENU_FK1] FOREIGN KEY([appmenu_uuid])
REFERENCES [dbo].[APPMENU] ([uuid]);

ALTER TABLE [dbo].[APPMENU] CHECK CONSTRAINT [APPMENU_APPMENU_FK1];

ALTER TABLE [dbo].[APPMENU]  WITH CHECK ADD  CONSTRAINT [APPMENU_SITEMAP_FK1] FOREIGN KEY([sitemap_uuid])
REFERENCES [dbo].[SITEMAP] ([uuid]);

ALTER TABLE [dbo].[APPMENU] CHECK CONSTRAINT [APPMENU_SITEMAP_FK1];

ALTER TABLE [dbo].[APPPAGE]  WITH CHECK ADD  CONSTRAINT [APPPAGE_APPLICATION_HEAD_FK1] FOREIGN KEY([application_head_uuid])
REFERENCES [dbo].[APPLICATION_HEAD] ([uuid]);

ALTER TABLE [dbo].[APPPAGE] CHECK CONSTRAINT [APPPAGE_APPLICATION_HEAD_FK1];

ALTER TABLE [dbo].[ATTENDANT]  WITH CHECK ADD  CONSTRAINT [ATTENDANT_FK1] FOREIGN KEY([company_uuid])
REFERENCES [dbo].[COMPANY] ([uuid]);

ALTER TABLE [dbo].[ATTENDANT] CHECK CONSTRAINT [ATTENDANT_FK1];

ALTER TABLE [dbo].[ATTENDANT]  WITH CHECK ADD  CONSTRAINT [ATTENDANT_FK2] FOREIGN KEY([department_uuid])
REFERENCES [dbo].[DEPARTMENT] ([uuid]);

ALTER TABLE [dbo].[ATTENDANT] CHECK CONSTRAINT [ATTENDANT_FK2];

ALTER TABLE [dbo].[ATTENDANT]  WITH CHECK ADD  CONSTRAINT [ATTENDANT_FK3] FOREIGN KEY([site_uuid])
REFERENCES [dbo].[SITE] ([uuid]);

ALTER TABLE [dbo].[ATTENDANT] CHECK CONSTRAINT [ATTENDANT_FK3];

ALTER TABLE [dbo].[DEPARTMENT]  WITH CHECK ADD  CONSTRAINT [DEPARTMENT_FK1] FOREIGN KEY([company_uuid])
REFERENCES [dbo].[COMPANY] ([uuid]);

ALTER TABLE [dbo].[DEPARTMENT] CHECK CONSTRAINT [DEPARTMENT_FK1];

ALTER TABLE [dbo].[DEPARTMENT]  WITH CHECK ADD  CONSTRAINT [DEPARTMENT_FK2] FOREIGN KEY([parent_department_uuid])
REFERENCES [dbo].[DEPARTMENT] ([uuid]);

ALTER TABLE [dbo].[DEPARTMENT] CHECK CONSTRAINT [DEPARTMENT_FK2];

ALTER TABLE [dbo].[DEPARTMENT]  WITH CHECK ADD  CONSTRAINT [DEPARTMENT_FK3] FOREIGN KEY([manager_uuid])
REFERENCES [dbo].[ATTENDANT] ([uuid]);

ALTER TABLE [dbo].[DEPARTMENT] CHECK CONSTRAINT [DEPARTMENT_FK3];

ALTER TABLE [dbo].[GROUP_APPMENU]  WITH CHECK ADD  CONSTRAINT [GROUP_APPMENU_APPMENU_FK1] FOREIGN KEY([appmenu_uuid])
REFERENCES [dbo].[APPMENU] ([uuid]);

ALTER TABLE [dbo].[GROUP_APPMENU] CHECK CONSTRAINT [GROUP_APPMENU_APPMENU_FK1];

ALTER TABLE [dbo].[GROUP_APPMENU]  WITH CHECK ADD  CONSTRAINT [GROUP_APPMENU_GROUP_HEAD_FK1] FOREIGN KEY([group_head_uuid])
REFERENCES [dbo].[GROUP_HEAD] ([uuid]);

ALTER TABLE [dbo].[GROUP_APPMENU] CHECK CONSTRAINT [GROUP_APPMENU_GROUP_HEAD_FK1];

ALTER TABLE [dbo].[GROUP_ATTENDANT]  WITH CHECK ADD  CONSTRAINT [GROUP_ATTENDANT_ATTENDANT_FK1] FOREIGN KEY([attendant_uuid])
REFERENCES [dbo].[ATTENDANT] ([uuid]);

ALTER TABLE [dbo].[GROUP_ATTENDANT] CHECK CONSTRAINT [GROUP_ATTENDANT_ATTENDANT_FK1];

ALTER TABLE [dbo].[GROUP_ATTENDANT]  WITH CHECK ADD  CONSTRAINT [GROUP_ATTENDANT_GROUP_HEA_FK1] FOREIGN KEY([group_head_uuid])
REFERENCES [dbo].[GROUP_HEAD] ([uuid]);

ALTER TABLE [dbo].[GROUP_ATTENDANT] CHECK CONSTRAINT [GROUP_ATTENDANT_GROUP_HEA_FK1];

ALTER TABLE [dbo].[GROUP_HEAD]  WITH CHECK ADD  CONSTRAINT [GROUP_HEAD_APPLICATION_HE_FK1] FOREIGN KEY([application_head_uuid])
REFERENCES [dbo].[APPLICATION_HEAD] ([uuid]);

ALTER TABLE [dbo].[GROUP_HEAD] CHECK CONSTRAINT [GROUP_HEAD_APPLICATION_HE_FK1];

ALTER TABLE [dbo].[GROUP_HEAD]  WITH CHECK ADD  CONSTRAINT [GROUP_HEAD_COMPANY_FK1] FOREIGN KEY([company_uuid])
REFERENCES [dbo].[COMPANY] ([uuid]);

ALTER TABLE [dbo].[GROUP_HEAD] CHECK CONSTRAINT [GROUP_HEAD_COMPANY_FK1];

ALTER TABLE [dbo].[SITE]  WITH CHECK ADD  CONSTRAINT [SITE_FK1] FOREIGN KEY([company_uuid])
REFERENCES [dbo].[COMPANY] ([uuid]);

ALTER TABLE [dbo].[SITE] CHECK CONSTRAINT [SITE_FK1];

ALTER TABLE [dbo].[SITE]  WITH CHECK ADD  CONSTRAINT [SITE_FK2] FOREIGN KEY([parent_site_uuid])
REFERENCES [dbo].[SITE] ([uuid]);

ALTER TABLE [dbo].[SITE] CHECK CONSTRAINT [SITE_FK2];

ALTER TABLE [dbo].[SITEMAP]  WITH CHECK ADD  CONSTRAINT [SITEMAP_APPLICATION_HEAD_FK1] FOREIGN KEY([application_head_uuid])
REFERENCES [dbo].[APPLICATION_HEAD] ([uuid]);

ALTER TABLE [dbo].[SITEMAP] CHECK CONSTRAINT [SITEMAP_APPLICATION_HEAD_FK1];

ALTER TABLE [dbo].[SITEMAP]  WITH CHECK ADD  CONSTRAINT [SITEMAP_APPPAGE_FK1] FOREIGN KEY([apppage_uuid])
REFERENCES [dbo].[APPPAGE] ([uuid]);

ALTER TABLE [dbo].[SITEMAP] CHECK CONSTRAINT [SITEMAP_APPPAGE_FK1];

ALTER TABLE [dbo].[SITEMAP]  WITH CHECK ADD  CONSTRAINT [SITEMAP_SITEMAP_FK1] FOREIGN KEY([sitemap_uuid])
REFERENCES [dbo].[SITEMAP] ([uuid]);

ALTER TABLE [dbo].[SITEMAP] CHECK CONSTRAINT [SITEMAP_SITEMAP_FK1];

ALTER TABLE [dbo].[SITEMAP]  WITH CHECK ADD  CONSTRAINT [SITEMAP_SITEMAP_FK2] FOREIGN KEY([root_uuid])
REFERENCES [dbo].[SITEMAP] ([uuid]);

ALTER TABLE [dbo].[SITEMAP] CHECK CONSTRAINT [SITEMAP_SITEMAP_FK2];



CREATE TABLE [dbo].[Proxy](
	[UUID] [varchar](36) NOT NULL,
	[Proxy_Action] [varchar](400) NOT NULL,
	[Proxy_Method] [varchar](400) NOT NULL,
	[description] [varchar](400) NOT NULL,
	[Proxy_Type] [varchar](50) NOT NULL,
	[need_redirect] [varchar](1) NOT NULL,
	[redirect_Proxy_Action] [varchar](400) NULL,
	[redirect_Proxy_Method] [varchar](400) NULL,
	[application_head_uuid] [varchar](36) NOT NULL,
	[redirect_src] [varchar](400) NULL,
 CONSTRAINT [PK_Proxy] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[APPMENU_PROXY_MAP](
	[uuid] [varchar](36) NOT NULL,
	[proxy_uuid] [varchar](36) NOT NULL,
	[appMenu_uuid] [varchar](36) NOT NULL,
 CONSTRAINT [PK_APPPAGE_PROXY_MAP_1] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[Proxy] ADD  CONSTRAINT [DF_Proxy_need_redirect]  DEFAULT ('N') FOR [need_redirect]
;
ALTER TABLE [dbo].[Proxy]  WITH CHECK ADD  CONSTRAINT [FK_Proxy_APPLICATION_HEAD] FOREIGN KEY([application_head_uuid])
REFERENCES [dbo].[APPLICATION_HEAD] ([uuid])
;
ALTER TABLE [dbo].[Proxy] CHECK CONSTRAINT [FK_Proxy_APPLICATION_HEAD]
;
ALTER TABLE [dbo].[APPMENU_PROXY_MAP]  WITH CHECK ADD  CONSTRAINT [FK_APPPAGE_PROXY_MAP_Proxy] FOREIGN KEY([proxy_uuid])
REFERENCES [dbo].[Proxy] ([UUID])
;
ALTER TABLE [dbo].[APPMENU_PROXY_MAP] CHECK CONSTRAINT [FK_APPPAGE_PROXY_MAP_Proxy]
;

CREATE TABLE [dbo].[SCHEDULE](
	[UUID] [varchar](36) NOT NULL,
	[SCHEDULE_NAME] [varchar](500) NOT NULL,
	[SCHEDULE_END_DATE] [datetime] NOT NULL,
	[LAST_RUN_TIME] [datetime] NULL,
	[LAST_RUN_STATUS] [varchar](10) NULL,
	[IS_CYCLE] [varchar](1) NOT NULL,
	[SINGLE_DATE] [datetime] NULL,
	[HOUR] [varchar](2) NULL,
	[MINUTE] [varchar](2) NULL,
	[CYCLE_TYPE] [varchar](100) NOT NULL,
	[C_MINUTE] [int] NULL,
	[C_HOUR] [int] NULL,
	[C_DAY] [int] NULL,
	[C_WEEK] [int] NULL,
	[C_DAY_OF_WEEK] [varchar](50) NULL,
	[C_MONTH] [int] NULL,
	[C_DAY_OF_MONTH] [varchar](50) NULL,
	[C_WEEK_OF_MONTH] [varchar](50) NULL,
	[C_YEAR] [int] NULL,
	[C_WEEK_OF_YEAR] [varchar](1024) NULL,
	[RUN_URL] [varchar](1024) NOT NULL,
	[RUN_URL_PARAMETER] [varchar](1024) NULL,
	[RUN_ATTENDANT_UUID] [varchar](36) NULL,
	[IS_ACTIVE] [varchar](1) NOT NULL,
	[START_DATE] [datetime] NULL,
	[RUN_SECURITY] [varchar](200) NULL,
	[EXPEND_ALL] [varchar](1) NULL,
	[contiune_dateTime] [datetime] NULL,
 CONSTRAINT [PK_SCHEDULE] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


CREATE TABLE [dbo].[SCHEDULE_TIME](
	[UUID] [varchar](36) NOT NULL,
	[SCHEDULE_UUID] [varchar](36) NOT NULL,
	[START_TIME] [datetime] NOT NULL,
	[FINISH_TIME] [datetime] NULL,
	[STATUS] [varchar](50) NULL,
 CONSTRAINT [PK_SCHEDULE_TIME] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];


ALTER TABLE [dbo].[SCHEDULE]  WITH CHECK ADD  CONSTRAINT [FK_SCHEDULE_ATTENDANT] FOREIGN KEY([RUN_ATTENDANT_UUID])
REFERENCES [dbo].[ATTENDANT] ([uuid]);

ALTER TABLE [dbo].[SCHEDULE] CHECK CONSTRAINT [FK_SCHEDULE_ATTENDANT];

ALTER TABLE [dbo].[SCHEDULE_TIME]  WITH CHECK ADD  CONSTRAINT [FK_SCHEDULE_TIME_SCHEDULE] FOREIGN KEY([SCHEDULE_UUID])
REFERENCES [dbo].[SCHEDULE] ([UUID]);

ALTER TABLE [dbo].[SCHEDULE_TIME] CHECK CONSTRAINT [FK_SCHEDULE_TIME_SCHEDULE];









