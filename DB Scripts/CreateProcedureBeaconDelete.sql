USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspDeleteBeacon]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDeleteBeacon]
    @pUuid uniqueidentifier,
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY
       DELETE FROM dbo.[Beacon] WHERE uuid = @pUuid

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

