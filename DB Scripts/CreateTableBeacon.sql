/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Standard Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [QS]
GO

/****** Object:  Table [dbo].[Beacon]    Script Date: 12/11/2017 18:23:32 ******/
DROP TABLE IF EXISTS [dbo].[Beacon]
GO

/****** Object:  Table [dbo].[Beacon]    Script Date: 12/11/2017 18:23:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Beacon](
	[uuid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Localization] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Beacon] PRIMARY KEY CLUSTERED 
(
	[uuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


