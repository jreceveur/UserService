USE UserServiceDemo
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Josh Receveur
-- Create date: June 11, 2016
-- Description:	List users
-- =============================================
CREATE PROCEDURE uspListUser

@email nvarchar(max)

AS
BEGIN

--return user information
SELECT USER_FNAME, USER_LNAME, USER_EMAIL, USER_PASSWORD, USER_GUID, USER_LAST_LOGIN FROM dbo.tbl_User where USER_EMAIL = @email

END
GO
