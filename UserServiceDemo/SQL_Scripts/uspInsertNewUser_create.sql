USE [UserServiceDemo]
GO
/****** Object:  StoredProcedure [dbo].[uspInsertNewUser]    Script Date: 6/11/2016 11:27:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Josh Receveur
-- Create date: June 11, 2016
-- Description:	Insert a new user into the database
-- =============================================
CREATE PROCEDURE [dbo].[uspInsertNewUser]

@fname nvarchar(50),
@lname nvarchar(50),
@password nvarchar(250),
@email nvarchar(50),
@guid uniqueidentifier

AS
BEGIN

--check if user exists by email adderss
SELECT * FROM dbo.tbl_User where USER_EMAIL = @email

declare @resultcount int = @@rowcount

-- if email adress does not return result
IF @resultcount = 0
BEGIN
--create new user, generate login date and GUID
	INSERT INTO dbo.tbl_User 
	(USER_FNAME, USER_LNAME, USER_PASSWORD, USER_EMAIL, USER_GUID, USER_LAST_LOGIN)
	VALUES
	(@fname, @lname, @password, @email, @guid, GETDATE())
END


END
