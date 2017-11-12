USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteUser]    Script Date: 11/11/2017 18:23:02 ******/
DROP PROCEDURE IF EXISTS [dbo].[uspDeleteUser]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteUser]    Script Date: 11/11/2017 18:23:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDeleteUser]
    @pId int,
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY
       DELETE FROM dbo.[User] WHERE id = @pId

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

