USE [QS]
GO

DROP PROCEDURE IF EXISTS [dbo].[uspEditContent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspEditContent]
	@pId int, 
    @pTitle NVARCHAR(64), 
    @pDescription NVARCHAR(256),
	@pUrl NVARCHAR(1024),
	@pStartDate DATE,
	@pEndDate DATE,
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY
		UPDATE dbo.[Content]
		SET 
			Title = @pTitle,
			Description = @pDescription,
			Url = @pUrl,
			StartDate = @pStartDate,
			EndDate = @pEndDate
		WHERE id = @pId

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO


