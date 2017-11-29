USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspDeleteContentBeacon]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDeleteContentBeacon]
    @pContentId int,
	@pBeaconId uniqueidentifier,
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY
		DELETE FROM dbo.[ContentBeacon] WHERE ContentId = @pContentId and BeaconId = @pBeaconId

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

