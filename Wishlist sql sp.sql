create table WishListTable
(
WishListId int identity(1,1) primary key,
BookId int not null,
FOREIGN KEY (BookId) REFERENCES BookTable(Id),
UserId int not null,
foreign key(UserId) REFERENCES Users(Id)
);
select * from WishListTable
create procedure [dbo].[SP_CreateWishList]
(
@BookId int,
@UserId int
)
as
begin
insert into WishListTable values
(
@BookId,@UserId
)
end
GO