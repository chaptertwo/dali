	Use Myth
	Go

	--if exists (select * from INFORMATION_SCHEMA.ROUTINES
	--		where Routine_Name = 'DbReset')
	--			Drop Procedure DbReset
	--Go
	

	Alter Procedure DbReset As
	Begin
		Delete From CreatureTrait;
		Delete From Footprint;
		Delete From Creature;
		Delete From Nest;
		Delete From Region;
		Delete From Trait;
		Delete From CreatureType;

		--DELETE FROM CreatureType;
		--DELETE FROM Trait;
		--DELETE FROM Region;
		--DELETE FROM Nest;
		--DELETE FROM Creature;
		--DELETE FROM Footprint;
		--DELETE FROM CreatureTrait;

		--DBCC CHECKIDENT ('CreatureType', RESEED, 1);
		--DBCC CHECKIDENT ('Trait', RESEED, 1);
		--DBCC CHECKIDENT ('Region', RESEED, 1);
		--DBCC CHECKIDENT ('Nest', RESEED, 1);
		--DBCC CHECKIDENT ('Creature', RESEED, 1);
		--DBCC CHECKIDENT ('Footprint', RESEED, 1);
		--DBCC CHECKIDENT ('CreatureTrait', RESEED, 1);

		--DBCC CHECKIDENT ('CreatureTrait', RESEED, 1);
		--DBCC CHECKIDENT ('Footprint', RESEED, 1);
		--DBCC CHECKIDENT ('Creature', RESEED, 1);
		--DBCC CHECKIDENT ('Nest', RESEED, 1);
		--DBCC CHECKIDENT ('Region', RESEED, 1);
		--DBCC CHECKIDENT ('Trait', RESEED, 1);
		--DBCC CHECKIDENT ('CreatureType', RESEED, 1);
		
		SET IDENTITY_INSERT CreatureType ON
		INSERT INTO CreatureType(TypeId, TypeName, Species, TypeDescription, FootprintType)
	VALUES (1, 'Ikiryō: Living Ghost', 'Eidolon', 'It is said a person under extreme duress may experience a split in their spirit; causing a part of them to be lost in time.', 'Ashes'),
	(2, 'Brownie', 'Fairy', 'Often heard scattering about at night, these helpful creatures perform various household tasks. Trying to catch one is straight-up immoral.', 'Footprints'),
	(3, 'Banshee', 'Eidolon', 'Their wails are soft as whispers until the moment they are seen.', 'Ashes'),
	(4, 'Deer Woman', 'Humanoid', 'A huntress with the body of a deer; known to allure indiginous people to their demise.', 'Footprints'),
	(5, 'Alkonost', 'Beast', 'They return from the underworld briefly to lay eggs on the sand. When one hatches a destructive storm appears from the sea.', 'Claw Marks'),
	(6, 'Sasquatch', 'Humanoid', 'These creatures are attracted to forests with strong magnetic fields --making them impossible to catch with a lense.', 'Footprints')
	SET IDENTITY_INSERT CreatureType OFF

	SET IDENTITY_INSERT Region ON
		INSERT INTO Region(RegionId, CountryAbbr, CountryFull, RegionLat, RegionLong)
	VALUES (1, 'JP', 'Japan', 36.204824, 138.252924), (2, 'UK', 'Scotland', 55.86515, -4.25763), (3, 'US', 'United States', 37.0902, 95.7129), (4, 'RU', 'Russia', 61.52401, 105.318756)
	SET IDENTITY_INSERT Region OFF

	SET IDENTITY_INSERT Nest ON
	INSERT INTO Nest(NestId, RegionId, NestLat, NestLong, NestName, IsPlaced)
	VALUES (1, 1, 35.764759, 137.309625, 'Mountain Trail', 1), (2, 2, 57.1472 , -2.097237, 'Aberdeen', 1), (3, 3, 47.723087, -86.940720, 'Superior', 1), (4, 3, 39.416669, -101.292105, 'The Plains', 1),
	(5, 4, 52.873623, 149.365817, 'Okhostk', 1), (6, 3, 40.2922222, -75.0594444, 'Trenton Grove', 1), (7, 3, null, null, 'Misted Forest', 0)
	SET IDENTITY_INSERT Nest OFF
		
		SET IDENTITY_INSERT Trait ON

	INSERT INTO Trait(TraitId, TraitName, TraitDescription, TraitDifficulty, IsPrimary)
	VALUES (1, 'Calm', 'Collected; this creature is slow to panic.', 2, 1), (2, 'Timid', 'Easy to scare; quick to retreat.', 3, 1), (3, 'Hardy', 'Has a strong will.', 5, 1),
	(4, 'Careful', 'Avoids open areas.', 5, 1), (6, 'Hasty', 'Wastes no time during travels', 3, 1), (7, 'Brave', 'Nothing frightens this one.', 2, 1)

	SET IDENTITY_INSERT Trait OFF

	
		SET IDENTITY_INSERT Creature ON
		INSERT INTO Creature(CreatureId, CreatureName, NestId, TypeId, TraitId, Picture, CreatureLat, CreatureLong, CreatureDescription, CreatureIsRevealed, CreatureHasNest, CreatureIsPlaced)
	VALUES(1, 'Maki''s Shadow', 1, 1, 1, 'https://lifeasahuman.com/files/2010/11/4385093787_c9daa3f878_o-550x367.jpg', 36.204824, 138.252924, NULL, 0, 1, 1),
	(2, 'Albert Fathom', 2, 2, 2, 'https://i.pinimg.com/originals/c2/6a/4d/c26a4d0130fa37fcfe9bdb048fe7cd1b.jpg', 57.1472 , -2.097237, NULL, 0, 1, 0),
	(3, 'Veiled One', 3, 3, 3, 'https://irishfolklore.files.wordpress.com/2017/10/kala.jpg?w=656', 47.723087, -86.940720, NULL, 0, 1, 0),
	(4, 'Kaya', 4, 4, 2, 'http://tve-static-syfy.nbcuni.com/prod/image/656/978/161205_3435981_Making_Magic__The_White_Lady_800x450_825317443984.jpg', 39.416669, -101.292105, NULL, 0, 1, 0),
	(5, 'Antonna', 7, 5, 3, 'https://i.pinimg.com/originals/ba/50/93/ba5093165b08e2d2b8ea0b317ed4c577.jpg', 52.873623, 149.365817, NULL, 0, 0, 0),
	(6, 'Leneck', 6, 6, 3, 'https://localtvwiti.files.wordpress.com/2015/02/bigfoot.jpg?quality=85&strip=all', 40.2922222, -75.0594444, NULL, 0, 0, 0)
	SET IDENTITY_INSERT Creature OFF

	
		--SET IDENTITY_INSERT CreatureTrait ON
		INSERT INTO CreatureTrait(CreatureId, TraitId)
	VALUES (1, 1), (1, 3), (2, 2), (3,3), (4,1), (4,2), (5,3), (6,2)
		--SET IDENTITY_INSERT CreatureTrait OFF
	
		SET IDENTITY_INSERT Footprint ON
		INSERT INTO Footprint(FootprintId, CreatureId, FootprintLat, FootprintLong, FootprintDate)
	VALUES (1, 1, 35.7654, 137.3309622, '2011-12-30'), (2, 1, 35.765116, 137.308906, '2011-12-30'), (3, 1, 35.764803, 137.311312, '2011-12-30'), 
	(4, 1, 35.76444, 137.311436, '2011-12-30'), (5, 1, 35.764594, 137.309219, '2011-12-30')
		
		SET IDENTITY_INSERT Footprint OFF




	End
	