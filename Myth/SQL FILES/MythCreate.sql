USE Master
GO

DROP DATABASE Myth
GO

CREATE DATABASE Myth
GO

USE Myth
GO

CREATE TABLE CreatureType(
	TypeId INT IDENTITY (1,1) PRIMARY KEY,
	TypeName NVARCHAR(30),
	Species NVARCHAR(30),
	TypeDescription NVARCHAR(190) NOT NULL,
	FootprintType VARCHAR(15)
	 )

CREATE TABLE Trait(
	TraitId INT IDENTITY (1,1) PRIMARY KEY,
	TraitName VARCHAR(30) NOT NULL,
	TraitDescription VARCHAR(90) NOT NULL,
	TraitDifficulty INT,
	IsPrimary bit )

CREATE TABLE Region(
	RegionId INT IDENTITY (1,1) PRIMARY KEY,
	CountryAbbr CHAR(2) NOT NULL,
	CountryFull NVARCHAR(30) NOT NULL,
	RegionLat DECIMAL (9,6) NOT NULL,
	RegionLong DECIMAL (9,6) NOT NULL )

CREATE TABLE Nest(
	NestId INT IDENTITY (1,1) PRIMARY KEY,
	RegionId INT FOREIGN KEY REFERENCES Region(RegionId),
	NestLat DECIMAL (9,6),
	NestLong DECIMAL (9,6),
	NestName NVARCHAR(70) NOT NULL,
	IsPlaced bit )

CREATE TABLE Creature(
	CreatureId INT IDENTITY (1,1) PRIMARY KEY,
	CreatureName NVARCHAR (30) NOT NULL,
	RegionId INT FOREIGN KEY REFERENCES Region(RegionId),
	NestId INT FOREIGN KEY REFERENCES Nest(NestId),
	TypeId INT FOREIGN KEY REFERENCES CreatureType(TypeId),
	TraitId INT FOREIGN KEY REFERENCES Trait(TraitId),
	Picture VARCHAR(300),
	CreatureLat DECIMAL (9,6),
	CreatureLong DECIMAL (9,6),
	CreatureDescription VARCHAR(90),
	CreatureIsRevealed bit,
	CreatureHasNest bit,
	CreatureIsPlaced bit
	 )

CREATE TABLE Footprint(
	FootprintId INT IDENTITY (1,1) PRIMARY KEY,
	CreatureId INT FOREIGN KEY REFERENCES Creature(CreatureId) NOT NULL,
	FootprintLat DECIMAL (9,6),
	FootprintLong DECIMAL (9,6),
	FootprintDate DATETIME2,
	FootprintIsRevealed bit )

	
CREATE TABLE CreatureTrait(
	CreatureId INT NOT NULL,
	TraitId INT NOT NULL,
	PRIMARY KEY (CreatureId, TraitId),
	CONSTRAINT FK_Creature_ID FOREIGN KEY (CreatureId) REFERENCES Creature(CreatureId),
	CONSTRAINT FK_Trait_ID FOREIGN KEY (TraitId) REFERENCES Trait(TraitId) )


