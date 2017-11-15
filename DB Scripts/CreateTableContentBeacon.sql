USE [QS]
GO

/****** Object:  Table [dbo].[ContentBeacon]    Script Date: 15/11/2017 14:27:31 ******/
DROP TABLE IF EXISTS [dbo].[ContentBeacon]
GO

/****** Object:  Table [dbo].[ContentBeacon]    Script Date: 15/11/2017 14:27:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContentBeacon](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ContentId] [int] NOT NULL,
	[BeaconId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ContentBeacon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ContentBeacon]  WITH CHECK ADD  CONSTRAINT [FK_ContentBeacon_Beacon] FOREIGN KEY([BeaconId])
REFERENCES [dbo].[Beacon] ([uuid])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ContentBeacon] CHECK CONSTRAINT [FK_ContentBeacon_Beacon]
GO

ALTER TABLE [dbo].[ContentBeacon]  WITH CHECK ADD  CONSTRAINT [FK_ContentBeacon_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ContentBeacon] CHECK CONSTRAINT [FK_ContentBeacon_Content]
GO


