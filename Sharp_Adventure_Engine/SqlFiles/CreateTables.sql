Use BinaryTextAdventure
Go

if exists (select * from sys.tables where name='PlayerCharacter')
		drop table PlayerCharacter
Go

if exists (select * from sys.tables where name='Outcome')
		drop table Outcome
Go

if exists (select * from sys.tables where name='EventChoice')
		drop table EventChoice
Go

if exists (select * from sys.tables where name='Scene')
		drop table Scene
Go

if exists (select * from sys.tables where name='Ending')
		drop table Ending
Go

if exists (select * from sys.tables where name='Game')
		drop table Game
Go

Create Table Game (
	GameId int identity(1,1) primary key not null,
	GameTitle nVarChar(75) not null,
	IntroText nVarChar(max) not null,
	Health int not null default 0,
	Gold int not null default 0

)
Go

Create Table Scene (
	SceneId int identity(1,1) primary key not null,
	GameId int not null foreign key references Game(GameId),
	IsStart bit not null,
	SceneName nVarChar(50) not null
)
Go

Create Table Ending(
	EndingId int identity(1, 1) primary key not null,
	GameId int not null foreign key references Game(GameId),
	EndingName nVarChar(128) not null,
	EndingText nVarChar(max) not null
)
Go

Create Table EventChoice (
	EventChoiceId int identity(1,1) primary key not null,
	SceneId int not null foreign key references Scene(SceneId),
	GenerationNumber int null,
	ImgUrl nVarChar(1000) null,
	EventName nVarChar(50) not null,
	StartText nVarChar(max) not null,
	PositiveText nVarChar(max) not null,
	NegativeText nVarChar(max) not null,
	PositiveRoute int null foreign key references EventChoice(EventChoiceId),
	NegativeRoute int null foreign key references EventChoice(EventChoiceId),
	PositiveButton nVarChar(128) not null,
	NegativeButton nVarChar(128) not null,
	PositiveSceneRoute int null foreign key references Scene(SceneId),
	NegativeSceneRoute int null foreign key references Scene(SceneId),
	PositiveEndingId int null foreign key references Ending(EndingId),
	NegativeEndingId int null foreign key references Ending(EndingId) 

)
Go

Create Table Outcome (
	OutcomeId int identity(1,1) primary key not null,
	EventChoiceId int not null foreign key references EventChoice(EventChoiceId),
	Positive bit not null,
	Health int not null default 0,
	Gold int not null default 0
)
Go

Create Table PlayerCharacter (
	CharacterId int identity(1,1) primary key not null,
	PlayerId nVarChar(128) not null foreign key references AspNetUsers(Id),
	SceneId int not null foreign key references Scene(SceneId),
	EventChoiceId int not null foreign key references EventChoice(EventChoiceId),
	CharacterName nVarChar(30) not null,
	HealthPoints int not null default 3,
	Gold int not null default 0
)
Go
