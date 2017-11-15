USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspEditUser]    Script Date: 11/11/2017 18:22:43 ******/
DROP PROCEDURE IF EXISTS [dbo].[uspEditBeacon]
GO

/****** Object:  StoredProcedure [dbo].[uspEditUser]    Script Date: 11/11/2017 18:22:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspEditBeacon]
	@pUuid uniqueidentifier, 
    @pName NVARCHAR(128), 
    @pLocalization NVARCHAR(64),
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY
		UPDATE dbo.[Beacon]
		SET 
			Name = @pName,
			Localization = @pLocalization
		WHERE uuid = @pUuid

		SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

