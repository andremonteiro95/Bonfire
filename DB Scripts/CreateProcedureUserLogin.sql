USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspLoginUser]    Script Date: 11/11/2017 18:22:26 ******/
DROP PROCEDURE [dbo].[uspLoginUser]
GO

/****** Object:  StoredProcedure [dbo].[uspLoginUser]    Script Date: 11/11/2017 18:22:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspLoginUser]
    @pEmail NVARCHAR(64), 
    @pPassword NVARCHAR(64),
    @response int OUTPUT
AS
BEGIN
	declare @salt as uniqueidentifier
	set @salt = (select Salt from dbo.[User] where Email = @pEmail)
    BEGIN TRY
       SET @response = CASE WHEN EXISTS (
							SELECT * FROM dbo.[User] 
							WHERE Email = @pEmail 
								AND Password = HASHBYTES('SHA2_512', @pPassword + CAST(@salt AS VARCHAR(36)))
						)
						THEN 1 ELSE 0
						END

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO


