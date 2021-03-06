CREATE TABLE [dbo].[app](
	[app_id] [int] IDENTITY(1,1) NOT NULL,
	[app_name] [nvarchar](255) NOT NULL,
	[active] [bit] NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK__app__app_id] PRIMARY KEY CLUSTERED 
(
	[app_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC__app__app_name] UNIQUE NONCLUSTERED 
(
	[app_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


