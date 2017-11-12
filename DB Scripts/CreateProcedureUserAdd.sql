USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspAddUser]    Script Date: 11/11/2017 18:23:10 ******/
DROP PROCEDURE IF EXISTS [dbo].[uspAddUser]
GO

/****** Object:  StoredProcedure [dbo].[uspAddUser]    Script Date: 11/11/2017 18:23:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspAddUser]
    @pName NVARCHAR(64), 
    @pEmail NVARCHAR(64), 
    @pPassword NVARCHAR(64),
	@pPrivilege int,
    @response int OUTPUT
AS
BEGIN

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[User] (Name, Email, Password, Salt, Privilege)
        VALUES(@pName, @pEmail, HASHBYTES('SHA2_512', @pPassword + CAST(@salt AS NVARCHAR(36))), @salt, @pPrivilege);

       SELECT @response = 1

    END TRY
    BEGIN CATCH
        SELECT @response = 0
    END CATCH

END
GO

