USE [UserServiceDemo]
GO
/****** Object:  StoredProcedure [dbo].[uspListUser]    Script Date: 6/11/2016 11:50:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Josh Receveur
-- Create date: June 11, 2016
-- Description:	List users
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateLoginDate]

@email nvarchar(50)

AS
BEGIN

--check if user exists by email adderss and return id
declare @id int = (SELECT ID FROM dbo.tbl_User where USER_EMAIL = @email)

declare @resultcount int = @@rowcount

-- if email adress does return result
IF @resultcount != 0
BEGIN
--update users last login to current date/time
	UPDATE dbo.tbl_User SET USER_LAST_LOGIN = GetDate() WHERE ID = @ID
END
 --return some error that user does not exist
END
