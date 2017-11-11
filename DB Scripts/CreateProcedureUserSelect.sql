USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspSelectUser]    Script Date: 11/11/2017 18:22:10 ******/
DROP PROCEDURE [dbo].[uspSelectUser]
GO

/****** Object:  StoredProcedure [dbo].[uspSelectUser]    Script Date: 11/11/2017 18:22:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspSelectUser]
	@pId int,
	@response int output
AS
BEGIN
    BEGIN TRY
        SELECT * FROM dbo.[User] WHERE id = @pId
		SET @response = 1
    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

