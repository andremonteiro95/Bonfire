USE [QS]
GO

/****** Object:  StoredProcedure [dbo].[uspAddUser]    Script Date: 11/11/2017 18:23:10 ******/
DROP PROCEDURE IF EXISTS [dbo].[uspAddBeacon]
GO

/****** Object:  StoredProcedure [dbo].[uspAddUser]    Script Date: 11/11/2017 18:23:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspAddBeacon]
    @pUuid uniqueidentifier, 
    @pName NVARCHAR(128), 
    @pLocation NVARCHAR(64),
    @response int OUTPUT
AS
BEGIN
    BEGIN TRY

        INSERT INTO dbo.[Beacon] (uuid, Name, Location)
        VALUES(@pUuid, @pName, @pLocation);

       SET @response = 1

    END TRY
    BEGIN CATCH
        SET @response = 0
    END CATCH

END
GO

