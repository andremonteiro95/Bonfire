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
        SELECT * FROM dbo.[Content]
		SET @response = 1
    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

