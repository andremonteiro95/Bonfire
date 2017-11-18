USE [QS]
GO

/****** Object:  Table [dbo].[Content]    Script Date: 15/11/2017 14:27:07 ******/
DROP TABLE IF EXISTS [dbo].[Content]
GO

/****** Object:  Table [dbo].[Content]    Script Date: 15/11/2017 14:27:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Content](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[Url] [nvarchar](1024) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


