USE [UserServiceDemo]
GO
/****** Object:  StoredProcedure [dbo].[uspUpdateUser]    Script Date: 6/11/2016 11:45:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Josh Receveur
-- Create date: June 11, 2016
-- Description:	Update a user that exists by email
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateUser]

@fname nvarchar(50),
@lname nvarchar(50),
@password nvarchar(250),
@email nvarchar(50),
@oldemail nvarchar(50)

AS
BEGIN

--check if user exists by email adderss and return id
declare @id int = (SELECT ID FROM dbo.tbl_User where USER_EMAIL = @oldemail)

declare @resultcount int = @@rowcount

-- if email adress does return result
IF @resultcount != 0
BEGIN
--update user with provided information
	UPDATE dbo.tbl_User SET USER_FNAME = @fname, USER_LNAME = @lname, USER_PASSWORD = @password, USER_EMAIL = @email where ID = @id
END

--generate some error to pass back to the client to let the user know that the user does not exist

END
