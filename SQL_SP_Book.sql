create table BookTable (
    ID int IDENTITY(1,1) PRIMARY KEY (ID),
    BookName varchar(50),
    Author varchar(50),
    BookImage varchar(100),
    BookDetail varchar(12),
	DiscountPrice int,
	ActualPrice int,
	Quantity int,
	Rating float,
	RatingCount float,

	);
go
create PROCEDURE [dbo].[CreateBook_SP] @BookName VARCHAR(100), @Author VARCHAR(100), @BookImage VARCHAR(100), @BookDetail varchar(100),@DiscountPrice int,
	@ActualPrice int,
	@Quantity int,
	@Rating float,
	@RatingCount float 

AS
BEGIN
INSERT INTO BookTable(BookName,Author,BookImage,BookDetail,DiscountPrice,ActualPrice,Quantity,Rating,RatingCount) VALUES (@BookName, @Author, @BookImage, @BookDetail,@DiscountPrice,@ActualPrice,@Quantity,@Rating,@RatingCount)
END


select * from Users
select * from BookTable
drop table BookTable


create procedure [dbo].[SP_GetBookByBookId]
(
@BookId int
)
as
begin
select * from BookTable where ID=@BookId
end
GO

create procedure [dbo].[SP_GetAllBook]
as
begin
select * from BookTable
end

create procedure [dbo].[SP_DeleteBook]
(
@BookId int
)
as
begin
Delete from BookTable where Id=@BookId;
end
GO


create procedure [dbo].[SP_UpdateBook]
(
@BooKId int,
@BookName varchar(50),
@Author varchar(50),
@BookImage varchar(100),
@BookDetail varchar(12),
@DiscountPrice int,
@ActualPrice int,
@Quantity int,
@Rating float,
@RatingCount float
)
as
begin
update BookTable set BookName =@BookName,Author=@Author,Rating=@Rating,RatingCount=@RatingCount,ActualPrice=@ActualPrice,
DiscountPrice=@DiscountPrice,BookDetail=@BookDetail,Quantity=@Quantity,BookImage=@BookImage
where Id=@BooKId;
end
GO


 "id": 13,
  "bookName": "The lord of the rings",
  "author": "lord",
  "bookImage": "https://res.cloudinary.com/ddterl1p6/image/upload/v1669836268/E-bookStore/The_lord_of_the_rings_xsrxxz.jpg",
  "bookDetail": "ringd",
  "discountPrice": 1000,
  "actualPrice": 1500,
  "quantity": 10,
  "rating": 5,
  "ratingCount": 3


SET IDENTITY_INSERT BookTable ON