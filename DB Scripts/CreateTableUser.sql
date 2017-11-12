USE [QS]
GO

ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF_User_Privilege]
GO

/****** Object:  Table [dbo].[User]    Script Date: 11/11/2017 18:21:05 ******/
DROP TABLE IF EXISTS [dbo].[User]
GO

/****** Object:  Table [dbo].[User]    Script Date: 11/11/2017 18:21:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Email] [nvarchar](64) NOT NULL,
	[Password] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NOT NULL,
	[Privilege] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Privilege]  DEFAULT ((0)) FOR [Privilege]
GO


