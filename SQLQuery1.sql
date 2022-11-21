select * from Users

create table Users (
    ID int IDENTITY(1,1) PRIMARY KEY (ID),
    FullName varchar(50),
    EmailId varchar(50),
    Password varchar(100),
    MobileNumber varchar(12));
select * from Users;
go
CREATE PROCEDURE [dbo].[Add_User] @FullName VARCHAR(100), @EmailId VARCHAR(100), @Password VARCHAR(100), @MobileNumber BIGINT 
AS
BEGIN
INSERT INTO Users(FullName,EmailId,Password,MobileNumber) VALUES (@FullName, @EmailId, @Password, @MobileNumber)
END

CREATE PROCEDURE [dbo].[Login_User] @EmailId VARCHAR(100), @Password VARCHAR (100)
AS
BEGIN
SELECT EmailId,Password FROM Users WHERE EmailId= @EmailId AND Password=@Password
END

CREATE PROCEDURE SP_ResetPassword @EmailId VARCHAR(100), @Password VARCHAR (100)
AS
BEGIN
UPDATE Users
SET Password= @Password where EmailId=@EmailId
END

create table AdminTable (
    ID int IDENTITY(1,1) PRIMARY KEY (ID),
    AdminName varchar(50),
    AdminEmail varchar(50),
    AdminPassword varchar(100),
    AdminMobileNumber varchar(12));
go
CREATE PROCEDURE [dbo].[Admin] @AdminName VARCHAR(100), @AdminEmail VARCHAR(100), @AdminPassword VARCHAR(100), @AdminMobileNumber BIGINT 
AS
BEGIN
INSERT INTO Admin (AdminName,AdminEmail,AdminPassword,AdminMobileNumber) VALUES (@AdminName, @AdminEmail, @AdminPassword, @AdminMobileNumber)
END

select * from AdminTable