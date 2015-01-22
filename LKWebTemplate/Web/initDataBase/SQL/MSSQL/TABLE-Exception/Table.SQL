CREATE TABLE [dbo].[ERROR_LOG](
	[uuid] [varchar](32) NOT NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
	[is_active] [varchar](1) NOT NULL,
	[error_code] [varchar](32) NOT NULL,
	[error_time] [varchar](32) NOT NULL,
	[error_message] [text] NOT NULL,
	[application_name] [varchar](200) NULL,
	[attendant_uuid] [varchar](32) NULL,
	[error_type] [varchar](32) NULL,
	[is_read] [varchar](1) NULL,
 CONSTRAINT [PK_ERROR_LOG] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];


ALTER TABLE [dbo].[ERROR_LOG] ADD  CONSTRAINT [DF__ERROR_LOG__creat__52593CB8]  DEFAULT (getdate()) FOR [create_date];

ALTER TABLE [dbo].[ERROR_LOG] ADD  CONSTRAINT [DF__ERROR_LOG__updat__534D60F1]  DEFAULT (getdate()) FOR [update_date];

ALTER TABLE [dbo].[ERROR_LOG] ADD  CONSTRAINT [DF_ERROR_LOG_is_read]  DEFAULT ('N') FOR [is_read];
