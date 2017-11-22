USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspEditBeacon]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspEditBeacon]
	@pUuid uniqueidentifier, 
    @pName NVARCHAR(128), 
    @pLocation NVARCHAR(64),
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY
		UPDATE dbo.[Beacon]
		SET 
			Name = @pName,
			Location = @pLocation
		WHERE uuid = @pUuid

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO


