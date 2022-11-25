create table Orders
(
         OrdersId int not null identity (1,1) primary key,
		 UserId INT NOT NULL,
		 FOREIGN KEY (UserId) REFERENCES Users(ID),
		 AddressId int
		 FOREIGN KEY (AddressId) REFERENCES AddressTable(AddressId),
	     BookId INT NOT NULL
		 FOREIGN KEY (BookId) REFERENCES BookTable(ID),
		 TotalPrice int,
		 BookQuantity int,
		 OrderDate Date
);

select * from Orders

create PROC SP_AddingOrders
	@UserId INT,
	@AddressId int,
	@BookId INT ,
	@BookQuantity int
AS
	Declare @TotPrice int
BEGIN
	Select @TotPrice=DiscountPrice from BookTable where ID = @BookId;
	IF (EXISTS(SELECT * FROM BookTable WHERE ID = @BookId))
	begin
		IF (EXISTS(SELECT * FROM Users WHERE ID = @UserId))
		Begin
		Begin try
			Begin transaction			
				INSERT INTO Orders(UserId,AddressId,BookId,TotalPrice,BookQuantity,OrderDate)
				VALUES ( @UserId,@AddressId,@BookId,@BookQuantity*@TotPrice,@BookQuantity,GETDATE())
				Update BookTable set Quantity=Quantity-@BookQuantity
				Delete from CartTable where BookId = @BookId and UserId = @UserId
			commit Transaction
		End try
		Begin catch
			Rollback transaction
		End catch
		end
		Else
		begin
			Select 1
		end
	end 
	Else
	begin
			Select 2
	end	
END
go


create PROC SP_GetAllOrders
	@UserId INT
AS
BEGIN
	select 
		ID,BookName,Author,DiscountPrice,ActualPrice,BookDetail,BookImage,Orders.OrdersId,Orders.OrderDate
		FROM BookTable
		inner join Orders
		on Orders.BookId=BookTable.ID where Orders.UserId=@UserId
END
go


create procedure SP_RemoveFromOrder
(
	@OrdersId int,
	@UserId int

)
as
declare @BookQuantity int,
		@BookId int
begin
		if(exists(select*from Orders where OrdersId=@OrdersId))
			begin
			select @BookId=BookId from Orders where OrdersId=@OrdersId and UserId=@UserId
			select @BookQuantity=BookQuantity from Orders where OrdersId=@OrdersId and UserId=@UserId
			update BookTable set Quantity=Quantity+@BookQuantity where ID=@BookId 
			delete from Orders where OrdersId=@OrdersId
			end
end