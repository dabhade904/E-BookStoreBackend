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
CREATE PROCEDURE [dbo].[SP_CreateBook] @BookName VARCHAR(100), @Author VARCHAR(100), @BookImage VARCHAR(100), @BookDetail varchar(100),@DiscountPrice int,
	@ActualPrice int,
	@Quantity int,
	@Rating float,
	@RatingCount float 
AS
BEGIN
INSERT INTO BookTable( BookName,Author,BookImage,BookDetail,DiscountPrice,ActualPrice,Quantity,Rating,RatingCount) VALUES (@BookName, @Author, @BookImage, @BookDetail,@DiscountPrice,@ActualPrice,@Quantity,@Rating,@RatingCount)
END

select * from BookTable
drop table BookTable


create procedure [dbo].[SP_GetBookByBookId]
(
@BooKId int
)
as
begin
select * from BookTable where Id=@BooKId
end
GO