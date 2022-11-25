create table FeedbackTable
(
FeedbackId int identity(1,1) primary key,
Rating int not null,
Comment varchar(350) not null,
BooKId int not null,
FOREIGN KEY (BookId) REFERENCES BookTable(ID),
UserId int not null,
foreign key(UserId) REFERENCES Users(ID)
);

select * from Users