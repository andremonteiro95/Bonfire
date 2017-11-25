USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspAddContent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspAddContent]
    @pTitle NVARCHAR(64), 
    @pDescription NVARCHAR(256), 
    @pUrl NVARCHAR(1024) NULL,
	@pStartDate date,
	@pEndDate date,
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY

        INSERT INTO dbo.[Content] (Title, Description, Url, StartDate, EndDate)
        VALUES(@pTitle, @pDescription, @pUrl, @pStartDate, @pEndDate);

       SET @response = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

