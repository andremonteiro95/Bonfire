USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspSelectAllBeacons]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspSelectAllBeacons]
	@response int output
AS
BEGIN
    BEGIN TRY
        SELECT * FROM dbo.[Beacon]
		SET @response = 1
    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

