CREATE TABLE [dbo].[ACTION_LOG](
	[uuid] [varchar](36) NOT NULL,
	[create_user] [varchar](36) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_user] [varchar](36) NOT NULL,
	[update_date] [datetime] NOT NULL,
	[is_active] [char](1) NOT NULL,
	[attendant_uuid] [varchar](36) NOT NULL,
	[class_name] [varchar](128) NULL,
	[function_name] [varchar](128) NULL,
	[parameter] [varchar](max) NULL
) ON [PRIMARY];