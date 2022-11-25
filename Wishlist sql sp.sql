create table WishList(
	WishListId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(ID),
	BookId int not null foreign key (BookId) references BookTable(ID)
	)
go

select * from WishList

create procedure SP_AddToWishList
(
	@UserId int,
	@BookId int
)
as
begin
insert into WishList
values( @UserId, @BookId);
end
go

select * from WishList
create procedure SP_GetAllWishList
(
	@UserId int
)
as
BEGIN
	select 
		w.WishListId,
		w.BookId,
		w.UserId,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice		
	from WishList w
	inner join BookTable b
	on w.BookId = b.ID
	where w.UserId = @UserId;
END
go 

select * from BookTable

create procedure SP_RemoveFromWishList
(
	@WishListId int
)
as
BEGIN 
	delete from WishList where WishListId = @WishListId;
END 
go

select * from AddressTable