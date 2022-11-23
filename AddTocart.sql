create table CartTable
(
CartId int identity(1,1) primary key,
BooksQty int not null,
BooKId int not null,
FOREIGN KEY (BookId) REFERENCES BookTable(Id),
UserId int not null,
foreign key(UserId) REFERENCES Users(Id)
);

select *from CartTable


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