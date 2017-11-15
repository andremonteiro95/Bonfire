USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspGetContentsByBeacon]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetContentsByBeacon]
	@pUuid uniqueidentifier,
	@response int output
AS
BEGIN
    BEGIN TRY
		SELECT c.id, c.Title, c.Description, c.Url FROM Content c
		INNER JOIN ContentBeacon cb ON cb.ContentId = c.id and cb.BeaconId = @pUuid;
		SET @response = 1
    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO


