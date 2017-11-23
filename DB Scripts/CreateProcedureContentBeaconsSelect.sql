USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspSelectContentBeacons]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspSelectContentBeacons]
	@pId int,
	@response int output
AS
BEGIN
    BEGIN TRY
        SELECT BeaconId FROM dbo.[ContentBeacon] WHERE ContentId = @pId
		SET @response = 1
    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

