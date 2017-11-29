USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspSelectAllContents]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspSelectAllContents]
	@response int output
AS
BEGIN
    BEGIN TRY
        select C.id, C.Title, C.Description, C.Url, C.StartDate, C.EndDate, Count(CB.BeaconId) as NumberOfBeacons from Content C
		LEFT JOIN ContentBeacon CB on C.id = CB.ContentId 
		GROUP BY C.id, C.Title, C.Description, C.Url, C.StartDate, C.EndDate
		SET @response = 1
    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

