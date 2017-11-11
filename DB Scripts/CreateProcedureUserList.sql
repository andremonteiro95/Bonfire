USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspSelectAllUsers]    Script Date: 11/11/2017 18:22:21 ******/
DROP PROCEDURE [dbo].[uspSelectAllUsers]
GO

/****** Object:  StoredProcedure [dbo].[uspSelectAllUsers]    Script Date: 11/11/2017 18:22:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspSelectAllUsers]
	@response int output
AS
BEGIN
    BEGIN TRY
        SELECT * FROM dbo.[User]
		SET @response = 1
    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

