create table CartTable
(
CartId int identity(1,1) primary key,
BooksQty int not null,
BooKId int not null,
FOREIGN KEY (BookId) REFERENCES BookTable(ID),
UserId int not null,
foreign key(UserId) REFERENCES Users(ID)
);

select * from CartTable
create procedure spGetAllCart
(
	@UserId int
)
as
BEGIN
	select 
		c.CartId,
		c.BookId,
		c.UserId,
		c.BooksQty,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice,
		b.Quantity
	from CartTable c
	inner join BookTable b
	on c.BookId = b.ID
	where c.UserId = @UserId;
END


create procedure SP_AddToBag
(
    @CartsQty int,
	@UserId int,
	@BookId int
)
as
BEGIN
IF (NOT EXISTS(SELECT * FROM CartTable	 WHERE BooKId = @BookId and UserId=@UserId))
		begin
		insert into CartTable
		values(@CartsQty, @UserId, @BookId);
		end
end
go


drop table CartTable
Users
select *from CartTable

create procedure spAddToCart
(
    @CartsQty int,
	@UserId int,
	@BookId int
)
as
BEGIN
IF (NOT EXISTS(SELECT * FROM CartTable	 WHERE BooKId = @BookId and UserId=@UserId))
		begin
		insert into CartTable
		values(@CartsQty, @UserId, @BookId);
		end
end
go

select * from BookTable
create procedure [dbo].[SP_AddCart]
(
@BooksQty int,
@BooKId int,
@UserId int
)
as
begin
insert into CartTable values
(
@BooksQty,@BooKId,@UserId
)
end
GO

create procedure [dbo].[SP_DeleteCart]
(
@CartId int
)
as
begin
Delete from CartTable where CartId=@CartId;
end
GO



create procedure [dbo].[SP_UpdateCart]
(
@CartId int,
@BooksQty int
)
as
begin
update CartTable set BooksQty=@BooksQty  where CartId=@CartId;
end
GO 

create procedure [dbo].[SP_GetCartBooks]
(
@UserId int
)
as
begin
select CT.CartId,BT.BookName,BT.Author,BT.Rating,BT.RatingCount,BT.ActualPrice,BT.DiscountPrice,BT.BookDetail,BT.Quantity,
BT.BookImage,CT.BooKId,CT.BooksQty
from CartTable as CT inner join BookTable as BT on BT.Id=CT.BookId
where CT.UserId=@UserId;
end
GO 

select * from BookTable