USE UserServiceDemo
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Josh Receveur
-- Create date: June 11, 2016
-- Description:	Delete a user that exists by email
-- =============================================
CREATE PROCEDURE uspDeleteUser

@fname nvarchar(50),
@lname nvarchar(50),
@password nvarchar(250),
@email nvarchar(50)

AS
BEGIN

--check if user exists by email adderss and return id
declare @id int = (SELECT ID FROM dbo.tbl_User where USER_EMAIL = @email)

declare @resultcount int = @@rowcount

-- if email adress does return result
IF @resultcount != 0
BEGIN
--update user with provided information
	DELETE FROM dbo.tbl_User WHERE ID = @ID
END

--generate some error to pass back to the client to let the user know that the user does not exist

END
GO
