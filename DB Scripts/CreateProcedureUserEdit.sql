USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspEditUser]    Script Date: 11/11/2017 18:22:43 ******/
DROP PROCEDURE [dbo].[uspEditUser]
GO

/****** Object:  StoredProcedure [dbo].[uspEditUser]    Script Date: 11/11/2017 18:22:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspEditUser]
	@pId int, 
    @pName NVARCHAR(64), 
    @pEmail NVARCHAR(64), 
    @pPassword NVARCHAR(64),
	@pPrivilege int,
    @response int OUTPUT
AS
BEGIN
	declare @salt as uniqueidentifier
	set @salt = (select Salt from dbo.[User] where id = @pId)
    BEGIN TRY
		IF(@pPassword IS NULL OR @pPassword = '')
			UPDATE dbo.[User]
			SET 
				Name = @pName,
				Email = @pEmail,
				Privilege = @pPrivilege
			WHERE id = @pId;

		ELSE
			UPDATE dbo.[User]
			SET 
				Name = @pName,
				Email = @pEmail,
				Password = HASHBYTES('SHA2_512', @pPassword + CAST(@salt AS NVARCHAR(36))),
				Privilege = @pPrivilege
			WHERE id = @pId;

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO


