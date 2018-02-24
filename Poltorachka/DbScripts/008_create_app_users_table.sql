CREATE TABLE [dbo].[app_users](
	[app_id] [int] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_app_users] PRIMARY KEY CLUSTERED 
(
	[app_id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


