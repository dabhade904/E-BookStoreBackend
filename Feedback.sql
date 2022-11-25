create table Feedback(
	FeedbackId int identity (1,1) primary key,
	Rating int not null,
	Comment varchar(max) not null,
	BookId int not null foreign key (BookId) references BookTable(ID),
	UserId int not null foreign key (UserId) references Users(ID)
	)

go

select * from Feedback
create procedure SP_AddFeedback
(
    @Rating int,
	@Comment varchar(max),
	@BookId int,
	@UserId int
)
as
BEGIN
insert into Feedback
values(@Rating, @Comment, @BookId, @UserId);
END
go

create procedure SP_GetAllFeedback
(
	@BookId int
)
as
BEGIN
	SELECT Feedback.FeedbackId,
		   Feedback.UserId,
		   Feedback.BookId,
		   Feedback.Comment,
		   Feedback.Rating, 
		   Users.FullName 
	FROM Users 
	INNER JOIN Feedback 
	ON Feedback.UserId = Users.ID WHERE BookId=@BookId

END
go