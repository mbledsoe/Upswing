create table Person
(
	Id uniqueidentifier not null,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	BirthDate date not null,
	CreatedAt datetime not null,

	constraint PK_Person primary key (Id)
)
go

create table Hobby
(
	Id uniqueidentifier not null,
	Title nvarchar(100) not null,
	CreatedAt datetime not null,

	constraint PK_Hobby primary key (Id)
)
go

create table PersonHobby
(
	Id uniqueidentifier not null,
	PersonId uniqueidentifier not null,
	HobbyId uniqueidentifier not null,
	CreatedAt datetime not null,

	constraint PK_PersonHobby primary key (Id),
	constraint FK_PersonHobby_Person foreign key (PersonId) references Person(Id),
	constraint FK_PersonHobby_Hobby foreign key (HobbyId) references Hobby(Id)
)
go