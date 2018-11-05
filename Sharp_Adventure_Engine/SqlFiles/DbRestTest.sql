Use BinaryTextAdventureTest
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
	Delete From AspNetUsers where Id in ('00000000-0000-0000-0000-000000000000', '10000000-0000-0000-0000-000000000000', '20000000-0000-0000-0000-000000000000');

	DBCC CHECKIDENT ('PlayerCharacter', RESEED, 1);
	DBCC CHECKIDENT ('Game', RESEED, 1);
	DBCC CHECKIDENT ('Scene', RESEED, 1);
	DBCC CHECKIDENT ('Outcome', RESEED, 1);
	DBCC CHECKIDENT ('Ending', RESEED, 1);
	DBCC CHECKIDENT ('EventChoice', RESEED, 1);
		
	Set Identity_Insert Game On;

	Insert Into Game (GameId, GameTitle, IntroText)
	Values
	('1', 'Fantasy Game', 'Hey there, this is a fantasy game.'),
	('2', 'Bullied Student Sim', 'Hey there, this is a school sim game where you get bullied.'),
	('3', 'Space Adventure Pt 13: The Arctonian Terradrax', 'Hey there, this is a space game where space stuff happens in space.')
		
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

	Insert Into EventChoice (EventChoiceId, SceneId, GenerationNumber, EventName, StartText,
	PositiveText, NegativeText, PositiveRoute, NegativeRoute, PositiveButton, NegativeButton,
	PositiveSceneRoute, NegativeSceneRoute, PositiveEndingId, NegativeEndingId)
	Values
	('1', '1', '0', 'Greeting Event', 'You are greeted in the foyer.', 'You are gracious to your host',
	 'You punch numerous holes in the foyer walls. A magical portal opens from one hole and you are teleported somewhere.', '2', null, 'Be gracious.', 'Punch holes in walls.',
	 null, '7', null, null),
	('2', '1', '1', 'Post-Greeting Event', 'You travel from the foyer to the dining room.', 'You enjoy a hearty meal',
	 'You have a heart attack.', null, null, 'Enjoy.', 'Don''t enjoy',
	 null, null, '3', '2'),
	 ('3', '7', '0', 'Fantasy Castle Event', 'You open your eyes to see an immense castle.', 'You bask in the glory of the castle',
	 'You are struck by a falling cherub and die.', null, null, 'Good Choice.', 'Bad Choice',
	 null, null, '3', '6'),
	 ('4', '2', '0', 'Classroom Greeting Event', 'You are greeted in the classroom.', 'You are gracious to your teacher.',
	 'You are ungracious to your teacher.', null, null, 'Be gracious.', 'Be ungracious.',
	 null, null, '5', '4'),
	 ('5', '3', '0', 'Space Greeting Event', 'You are greeted in space.', 'You are gracious to space.',
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
	
	Insert Into AspNetUsers (Id)
	Values
	('00000000-0000-0000-0000-000000000000'),
	('10000000-0000-0000-0000-000000000000'),
	('20000000-0000-0000-0000-000000000000')

	Set Identity_Insert PlayerCharacter On;
	
	Insert Into PlayerCharacter (CharacterId, PlayerId, SceneId, EventChoiceId, CharacterName, HealthPoints, Gold)
	Values
	('1', '00000000-0000-0000-0000-000000000000', '1', '1', 'Player''s Character', '5', '12'),
	('2', '10000000-0000-0000-0000-000000000000', '2', '4', 'Lady Character', '3', '4'),
	('3', '20000000-0000-0000-0000-000000000000', '3', '5', 'Guy Character', '1', '0')
		
	Set Identity_Insert PlayerCharacter Off;







End
