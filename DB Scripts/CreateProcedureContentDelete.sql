USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspDeleteContent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDeleteContent]
    @pId int,
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY
       DELETE FROM dbo.[Content] WHERE id = @pId

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

