USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspAddContentBeacon]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspAddContentBeacon]
    @pContentId int, 
    @pBeaconId uniqueidentifier,
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY

		IF NOT EXISTS ( SELECT 1 FROM [dbo].[ContentBeacon] WHERE ContentId = @pContentId and BeaconId = @pBeaconId )
		BEGIN
			INSERT INTO dbo.[ContentBeacon] (ContentId, BeaconId)
			VALUES(@pContentId, @pBeaconId);
		END

       SET @response = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

