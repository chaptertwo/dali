Use BinaryTextAdventure
Go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
		where Routine_Name = 'DbReset')
			Drop Procedure DbReset
Go

Create Procedure DbReset As
Begin
	Delete From PlayerCharacter;
	Delete From Outcome;
	Delete From EventChoice;
	Delete From Ending;
	Delete From Scene;
	Delete From Game;
	Delete From AspNetUsers where Id in ('00000000-0000-0000-0000-000000000000', '10000000-0000-0000-0000-000000000000', '20000000-0000-0000-0000-000000000000', 'a60f9be0-baff-48c7-beb6-049114db1a2d', '484946c7-b906-4a39-8b15-58e5629ac87e', '1cb4258e-b0ca-4fae-b746-e831f0cab106');

	DBCC CHECKIDENT ('PlayerCharacter', RESEED, 1);
	DBCC CHECKIDENT ('Game', RESEED, 1);
	DBCC CHECKIDENT ('Scene', RESEED, 1);
	DBCC CHECKIDENT ('Outcome', RESEED, 1);
	DBCC CHECKIDENT ('Ending', RESEED, 1);
	DBCC CHECKIDENT ('EventChoice', RESEED, 1);
		
	Set Identity_Insert Game On;

	Insert Into Game (GameId, GameTitle, IntroText, Health, Gold)
	Values
	('1', 'Fantasy Game', 'Hey there, this is a fantasy game.', 3, 2),
	('2', 'Bullied Student Sim', 'Hey there, this is a school sim game where you get bullied.', 5, 1),
	('3', 'Space Adventure Pt 13: The Arctonian Terradrax', 'Hey there, this is a space game where space stuff happens in space.', 2, 5)
		
	Set Identity_Insert Game Off;	
	
	Set Identity_Insert Scene On;

	Insert Into Scene (SceneId, GameId, IsStart, SceneName)
	Values
	('1', '1', '1', 'The Foyer'),
	('2', '2', '1', 'The Classroom'),
	('3', '3', '1', 'The Space Restaurant'),
	('4', '3', '1', 'The Thingverse'),
	('5', '3', '0', 'The Space Store'),
	('6', '3', '0', 'The Space Planet'),
	('7', '1', '1', 'The Magic Castle')
		
	Set Identity_Insert Scene Off;

	Set Identity_Insert Ending On;
	
	Insert Into Ending (EndingId, GameId, EndingName, EndingText)
	Values
	('1', '3', 'The End', 'The end is at hand.'),
	('2', '1', 'Bad End', 'Bad Ending here. Hi.'),
	('3', '1', 'Yo!', 'Yo. I''m the good ending.'),
	('4', '2', 'Sup.', 'Just end number 4 here. Sup.'),
	('5', '2', 'Fin', 'It''s over. You can leave now.'),
	('6', '1', 'Yawn', 'You have bored me. Leave now.'),
	('7', '3', 'Behold The Might Of Space', 'Space retaliates. You have "dead" now.')
		
	Set Identity_Insert Ending Off;

	Set Identity_Insert EventChoice On;

	Insert Into EventChoice (EventChoiceId, SceneId, GenerationNumber, ImgUrl, EventName, StartText,
	PositiveText, NegativeText, PositiveRoute, NegativeRoute, PositiveButton, NegativeButton,
	PositiveSceneRoute, NegativeSceneRoute, PositiveEndingId, NegativeEndingId)
	Values
	('1', '1', '0', 'https://static1.squarespace.com/static/5a023d01ace864fd7fef3370/5a025338a6525a7eeaae3341/5a02532ea6525a7eeaae30fe/1513028424278/Grand+Stir+Hall%2C+Black+and+White+Marble+Foyer%2C', 'Greeting Event', 'You are greeted in the foyer.', 'You are gracious to your host',
	 'You punch numerous holes in the foyer walls. A magical portal opens from one hole and you are teleported somewhere.', '2', null, 'Be gracious.', 'Punch holes in walls.',
	 null, '7', null, null),
	('2', '1', '1', 'https://images2.roomstogo.com/is/image/roomstogo/dr_rm_hillcreek_black_6_chrs_~Hill-Creek-Black-5-Pc-Rectangle-Dining-Room.jpeg?$pdp_gallery_945$', 'Post-Greeting Event', 'You travel from the foyer to the dining room.', 'You enjoy a hearty meal',
	 'You have a heart attack.', null, null, 'Enjoy.', 'Don''t enjoy',
	 null, null, '3', '2'),
	 ('3', '7', '0', 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/Alcazar_de_Segovia.JPG/1200px-Alcazar_de_Segovia.JPG', 'Fantasy Castle Event', 'You open your eyes to see an immense castle.', 'You bask in the glory of the castle',
	 'You are struck by a falling cherub and die.', null, null, 'Good Choice.', 'Bad Choice',
	 null, null, '3', '6'),
	 ('4', '2', '0', 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/26/Andrew_Classroom_De_La_Salle_University.jpeg/1200px-Andrew_Classroom_De_La_Salle_University.jpeg', 'Classroom Greeting Event', 'You are greeted in the classroom.', 'You are gracious to your teacher.',
	 'You are ungracious to your teacher.', null, null, 'Be gracious.', 'Be ungracious.',
	 null, null, '5', '4'),
	 ('5', '3', '0', 'https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/International_Space_Station_after_undocking_of_STS-132.jpg/1200px-International_Space_Station_after_undocking_of_STS-132.jpg', 'Space Greeting Event', 'You are greeted in space.', 'You are gracious to space.',
	 'You snidely scoff at space.', null, null, 'Be gracious.', 'Scoff',
	 null, null, '1', '7')
		
	Set Identity_Insert EventChoice Off;
	
	Set Identity_Insert Outcome On;

	Insert Into Outcome (OutcomeId, EventChoiceId, Positive, Health, Gold)
	Values
	('1', '1', 1, 0, 10),
	('2', '1', 0, -1, 0),
	('3', '2', 1, 1, 0),
	('4', '2', 0, 0, -10),
	('5', '3', 1, 0, 0),
	('6', '3', 0, 1, 5),
	('7', '4', 1, -1, 1),
	('8', '4', 0, 2, 4)
		
	Set Identity_Insert Outcome Off;
	
	Insert Into AspNetUsers (Id, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	Values
	('00000000-0000-0000-0000-000000000000', '1', '1', '1', '1', '1', 'Alvin'),
	('10000000-0000-0000-0000-000000000000', '1', '1', '1', '1', '1', 'Simon'),
	('20000000-0000-0000-0000-000000000000', '1', '1', '1', '1', '1', 'Piotr')

	INSERT INTO AspNetUsers
	VALUES ('a60f9be0-baff-48c7-beb6-049114db1a2d', 'admin@admin.com', 0, 'AF3Hwrh0a/lEb9IdlaGukHdXenGAR/XMV3v0bWs65PeLy7/sdOgeMcgnmbTNDOXu7g==', '0eb698b8-db7a-49ee-9489-ad0b1e923bf6', null, 0, 0, null, 1, 0, 'admin@admin.com', 'test', 'test'),
	('484946c7-b906-4a39-8b15-58e5629ac87e', 'user@user.com', 0, 'ADaS3zo0G3ILfELYHAJF8j3ngjmXJNqr04vxVwKfdkjbDtkkA5ks41BElXP49UybqQ==', '2937b5c7-b664-48da-a706-8e2f85821299', null, 0, 0, null, 1, 0, 'user@user.com', 'user', 'user'),
	('1cb4258e-b0ca-4fae-b746-e831f0cab106', 'creator@creator.com', 0, 'ANVmyOY5sJgmHtjlIzq0NuZ9MKlXP2GzBrdTN3oWBA+YJLzi2R5DH/hp6V9y6RORFw==', '6465c93e-597c-444d-b1f6-9450064c2735', null, 0, 0, null, 1, 0, 'creator@creator.com', 'creator', 'creator')


	Set Identity_Insert PlayerCharacter On;

	Insert Into PlayerCharacter (CharacterId, PlayerId, SceneId, EventChoiceId, CharacterName, HealthPoints, Gold)
	Values
	('1', '00000000-0000-0000-0000-000000000000', '1', '1', 'Player''s Character', '5', '12'),
	('2', '10000000-0000-0000-0000-000000000000', '2', '4', 'Lady Character', '3', '4'),
	('3', '20000000-0000-0000-0000-000000000000', '3', '5', 'Guy Character', '1', '0')
	--('4', '89cfef75-a0ab-487e-8bc1-7850865bc2e3', '2', '4', 'Test Char','10' , '10'),
	--('5', '89cfef75-a0ab-487e-8bc1-7850865bc2e3', '1', '3', 'Test Char NUMBER 2','15' , '1')

		
	Set Identity_Insert PlayerCharacter Off;
	
	Insert Into AspNetRoles(Id,[Name])
	VALUES(1, 'Admin'),
		(2, 'Creator'),
		(3, 'User')

	INSERT INTO AspNetUserRoles(UserId, RoleId)
	VALUES('a60f9be0-baff-48c7-beb6-049114db1a2d', 2), ('a60f9be0-baff-48c7-beb6-049114db1a2d', 1),
	('484946c7-b906-4a39-8b15-58e5629ac87e', 3),
	('1cb4258e-b0ca-4fae-b746-e831f0cab106', 2), ('1cb4258e-b0ca-4fae-b746-e831f0cab106', 3)

	UPDATE PlayerCharacter
	SET PlayerId = 'a60f9be0-baff-48c7-beb6-049114db1a2d'
	WHERE CharacterId = 1

	UPDATE PlayerCharacter
	SET PlayerId = '484946c7-b906-4a39-8b15-58e5629ac87e'
	WHERE CharacterId = 2

	UPDATE PlayerCharacter
	SET PlayerId = '1cb4258e-b0ca-4fae-b746-e831f0cab106'
	WHERE CharacterId = 3



End
