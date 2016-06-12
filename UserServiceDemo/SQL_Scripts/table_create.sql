create table tbl_User(
ID int NOT NULL IDENTITY (1,1) PRIMARY KEY,
USER_GUID uniqueidentifier,
USER_FNAME nvarchar(50) NOT NULL,
USER_LNAME nvarchar(50) NOT NULL,
USER_PASSWORD nvarchar(250) NOT NULL,
USER_EMAIL nvarchar(250) NOT NULL,
USER_LAST_LOGIN smalldatetime
)