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

create procedure [dbo].[SP_GetAllWishListBooks]
(
@BookId int
)
as
begin
select * from BookTable where Id=@BookId
end
GO


create procedure [dbo].[SP_DeleteWishlist]
(
@WishListId int
)
as
begin
Delete from WishListTable where WishListId=@WishListId;
end
GO



select * from AdminData
select * from WishListTable
select * from users
select * from BookTable

create procedure spGetAllWishList(	
@UserId int
)
as
BEGIN
select w.WishListId,w.BookId,w.UserId,b.BookName,b.BookImage,b.Author,b.DiscountPrice,b.ActualPrice	from WishList w	inner join Books b	
on 
w.BookId = b.BookId	where w.UserId = @UserId;
END
