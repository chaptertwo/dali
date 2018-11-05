USE Master
GO

USE Myth
GO

INSERT INTO CreatureType(TypeName, Species, TypeDescription, FootprintType)
VALUES ('Ikiryō: Living Ghost', 'Eidolon', 'It is said a person under extreme duress may experience a split in their spirit; causing a part of them to be lost in time.', 'Ashes'),
('Brownie', 'Fairy', 'Often heard scattering about at night, these helpful creatures perform various household tasks. Trying to catch one is straight-up immoral.', 'Footprints'),
('Banshee', 'Eidolon', 'Their wails are soft as whispers until the moment they are seen.', 'Ashes'),
('Deer Woman', 'Humanoid', 'A huntress with the body of a deer; known to allure indiginous people to their demise.', 'Footprints'),
('Alkonost', 'Beast', 'They return from the underworld briefly to lay eggs on the sand. When one hatches a destructive storm appears from the sea.', 'Claw Marks'),
('Sasquatch', 'Humanoid', 'These creatures are attracted to forests with strong magnetic fields --making them impossible to catch with a lense.', 'Footprints')

GO

INSERT INTO Region(CountryAbbr, CountryFull, RegionLat, RegionLong)
VALUES ('JP', 'Japan', 36.204824, 138.252924), ('UK', 'Scotland', 55.86515, -4.25763), ('US', 'United States', 37.0902, 95.7129), ('RU', 'Russia', 61.52401, 105.318756)
GO

INSERT INTO Nest(RegionId, NestLat, NestLong, NestName, IsPlaced)
VALUES (1, 35.764759, 137.309625, 'Mountain Trail', 1), (2, 57.1472 , -2.097237, 'Aberdeen', 1), (3, 47.723087, -86.940720, 'Superior', 1), (3, 39.416669, -101.292105, 'The Plains', 1),
(4, 52.873623, 149.365817, 'Okhostk', 1), (3, 40.2922222, -75.0594444, 'Trenton Grove', 1), (3, null, null, 'Misted Forest', 0)
GO

SET IDENTITY_INSERT Trait ON
GO
INSERT INTO Trait(TraitId, TraitName, TraitDescription, TraitDifficulty, IsPrimary)
VALUES (1, 'Calm', 'Collected; this creature is slow to panic.', 2, 1), (2, 'Timid', 'Easy to scare; quick to retreat.', 3, 1), (3, 'Hardy', 'Has a strong will.', 5, 1),
(4, 'Careful', 'Avoids open areas.', 5, 1), (6, 'Hasty', 'Wastes no time during travels', 3, 1), (7, 'Brave', 'Nothing frightens this one.', 2, 1)
GO
SET IDENTITY_INSERT Trait OFF
GO

INSERT INTO Creature(CreatureName, NestId, TypeId, TraitId, Picture, CreatureLat, CreatureLong, CreatureDescription, CreatureIsRevealed, CreatureHasNest, CreatureIsPlaced)
VALUES('Maki''s Shadow', 1, 1, 1, 'https://lifeasahuman.com/files/2010/11/4385093787_c9daa3f878_o-550x367.jpg', 36.204824, 138.252924, NULL, 0, 1, 1),
('Albert Fathom', 2, 2, 2, 'https://i.pinimg.com/originals/c2/6a/4d/c26a4d0130fa37fcfe9bdb048fe7cd1b.jpg', 57.1472 , -2.097237, NULL, 0, 1, 0),
('Veiled One', 3, 3, 3, 'https://irishfolklore.files.wordpress.com/2017/10/kala.jpg?w=656', 47.723087, -86.940720, NULL, 0, 1, 0),
('Kaya', 4, 4, 2, 'http://tve-static-syfy.nbcuni.com/prod/image/656/978/161205_3435981_Making_Magic__The_White_Lady_800x450_825317443984.jpg', 39.416669, -101.292105, NULL, 0, 1, 0),
('Antonna', 7, 5, 3, 'https://i.pinimg.com/originals/ba/50/93/ba5093165b08e2d2b8ea0b317ed4c577.jpg', 52.873623, 149.365817, NULL, 0, 0, 0),
('Leneck', 6, 6, 3, 'https://localtvwiti.files.wordpress.com/2015/02/bigfoot.jpg?quality=85&strip=all', 40.2922222, -75.0594444, NULL, 0, 0, 0)


GO

INSERT INTO CreatureTrait(CreatureId, TraitId)
VALUES (1, 1), (1, 3), (2, 2), (3,3), (4,1), (4,2), (5,3), (6,2)
GO

--REMEMBER THIS IS TEST DATA,, NOT ACCURATE FOR GAME!
INSERT INTO Footprint(CreatureId, FootprintLat, FootprintLong, FootprintDate)
VALUES (1, 35.7654, 137.3309622, '2011-12-30'), (1, 35.765116, 137.308906, '2011-12-30'), (1, 35.764803, 137.311312, '2011-12-30'), 
(1, 35.76444, 137.311436, '2011-12-30'), (1, 35.764594, 137.309219, '2011-12-30')
--(2, 57.1458 , -2.097220, '2011-12-30'), 
--(3, 47.723187, -86.940820, NULL), (4, 39.416769, -101.293105,  NULL),
--(6, 40.2923222, -75.0593444, NULL)
GO









--select*
--from Nest



--select *
--from Creature

--select *
--from Footprint

--SELECT c.CreatureId, f.FootprintId, f.FootprintDate, f.FootprintIsRevealed, f.FootprintLat, f.FootprintLong, 
--                c.CreatureName, c.CreatureIsRevealed, c.CreatureLat, c.CreatureLong FROM Footprint f 
--                FULL OUTER JOIN Creature c ON c.CreatureId = f.CreatureId 
--                WHERE c.CreatureId = 3



--select *
--from CreatureTrait



--DELETE FROM CreatureTrait WHERE CreatureId = 3
--DELETE FROM Footprint WHERE CreatureId = 3
--DELETE FROM Creature WHERE CreatureId = 3
--go

--select *
--from AspNetRoles
--select*
--from AspNetUserRoles
--Select *
--FROM AspNetUsers
 --846c7980-3484-4a18-a596-0c1838a70b73
 --4c2163e9-0699-4013-b6b8-def618bfdeb4
--insert into CreatureTrait(TraitId, CreatureId)
--values(2,3)
--go

--UPDATE AspNetUsers SET
--Email = 'admin@yahoo.com'
--WHERE
--Id = '846c7980-3484-4a18-a596-0c1838a70b73'


--select*
--from AspNetUsers
--select*
--from AspNetUserRoles
--select*
--from AspNetRoles

--SELECT c.CreatureId, f.FootprintId, f.FootprintDate, f.FootprintIsRevealed, f.FootprintLat, f.FootprintLong, f.FootprintType, c.CreatureName, c.CreatureIsRevealed, c.CreatureLat, c.CreatureLong
--FROM Footprint f
--FULL OUTER JOIN Creature c ON c.CreatureId = f.CreatureId
--WHERE c.CreatureId = 1

--select*
--from Footprint

--FULL OUTER JOIN CreatureTrait ct ON ct.CreatureId = c.CreatureId

--select*
--from Nest
--UPDATE Nest
--SET IsPlaced = 0
--WHERE NestId = 7

--Select c.CreatureId, c.CreatureName, t.TypeName, t.Species, t.TypeDescription, trait.TraitName, trait.TraitDescription, c.CreatureIsRevealed, n.NestName
--FROM Creature c
--INNER JOIN CreatureType t ON t.TypeId = c.TypeId
--FULL OUTER JOIN CreatureTrait ct ON ct.CreatureId = c.CreatureId
--FULL OUTER JOIN Trait trait ON trait.TraitId = ct.TraitId
--INNER JOIN Nest n ON n.NestId = c.NestId

--Select c.CreatureId, c.CreatureName, t.TypeName, t.Species, t.TypeDescription, trait.TraitName, trait.TraitDescription, c.CreatureIsRevealed, n.NestName, f.FootprintLat, f.FootprintLong, f.FootprintType, f.FootprintIsRevealed
--FROM Creature c
--INNER JOIN CreatureType t ON t.TypeId = c.TypeId
--FULL OUTER JOIN CreatureTrait ct ON ct.CreatureId = c.CreatureId
--FULL OUTER JOIN Trait trait ON trait.TraitId = ct.TraitId
--INNER JOIN Nest n ON n.NestId = c.NestId
--FULL OUTER JOIN Footprint f ON f.CreatureId = c.CreatureId

--select c.CreatureId, c.CreatureName, t.TraitName, t.TraitDescription
--FROM CreatureTrait ct
--INNER JOIN Creature c ON c.CreatureId = ct.CreatureId
--INNER JOIN Trait t ON t.TraitId = ct.TraitId


--select*
--from CreatureTrait
--where CreatureTrait.CreatureId = 1

--select t.TraitId, t.TraitName, t.TraitDescription
--FROM Trait t
--INNER JOIN CreatureTrait c ON c.TraitId = t.TraitId
--WHERE c.CreatureId = 1


--SELECT *
--FROM Creature c
--left JOIN Footprint f ON f.CreatureId = c.CreatureId
--left JOIN Region r ON r.RegionId = c.RegionId
--left JOIN Nest n ON n.RegionId = c.RegionId
--left JOIN Trait tr on tr.TraitId = c.TraitId
--left JOIN CreatureType tt on tt.TypeId = c.TypeId

--select t.NestId, t.NestName, t.NestLat, t.NestLong FROM Nest t
--INNER JOIN Creature c ON c.NestId = t.NestId
--WHERE c.CreatureId = 1

--select *
--from Footprint