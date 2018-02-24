CREATE TABLE [dbo].[individual](
	[ind_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[user_id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_individual] PRIMARY KEY CLUSTERED 
(
	[ind_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_individual] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[fact] (
	[fact_id] [int] IDENTITY(1,1) NOT NULL,
	[loser_id] [int] NOT NULL,
	[winner_id] [int] NOT NULL,
	[creator_id] [int] NOT NULL,
	[approver_id] [int] NULL,
	[status] [tinyint] NOT NULL,
	[score] [int] NOT NULL,
    [description] NVARCHAR(255) NULL,
	[date] [datetime2](7) NOT NULL,

 CONSTRAINT [PK_fact] PRIMARY KEY CLUSTERED 
(
	[fact_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

