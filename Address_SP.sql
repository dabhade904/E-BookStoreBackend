--create addressType table
create Table AddressTypeTable
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(300) 
);
select count(*) from AddressTypeTable

insert into AddressTypeTable values('Home'),('Office'),('Other');

---create address table
create Table AddressTable
(
AddressId INT IDENTITY(1,1) PRIMARY KEY,
Address varchar(300),
City varchar(250),
State varchar(350),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeTable(TypeId),
UserId INT 
FOREIGN KEY (UserId) REFERENCES Users(Id)
);

select * from AddressTable

create procedure SP_AddAddress
(
@Address varchar(300),
@City varchar(250),
@State varchar(350),
@TypeId int,
@UserId int
)
as
begin
Insert into AddressTable 
values(@Address, @City, @State, @TypeId, @UserId);
end
go

create procedure SP_UpdateAddress
(
@AddressId int,
@Address varchar(300),
@City varchar(250),
@State varchar(250),
@TypeId int
)
as
BEGIN
Update AddressTable set
Address = @Address, City = @City,
State = @State , TypeId = @TypeId
where AddressId = @AddressId
end
go


create Procedure SP_DeleteAddress
(
@AddressId int
)
as
BEGIN
Delete from AddressTable where AddressId = @AddressId 
end
go


create procedure [dbo].[SP_GetAllAddress]
(
@UserId int
)
as
begin
select AT.AddressId,AT.Address,AT.City,AT.State,AT.TypeId,AT.UserId,ATT.AddressType
from AddressTable as AT inner join AddressTypeTable as ATT on AT.TypeId=ATT.TypeId
where AT.UserId=@UserId;
end
GO

