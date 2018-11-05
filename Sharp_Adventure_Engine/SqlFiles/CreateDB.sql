SET NOCOUNT ON
Go

USE master
Go

if exists (select * from sysdatabases where name='BinaryTextAdventure')
		drop database BinaryTextAdventure
Go

Create Database BinaryTextAdventure
Go

Use BinaryTextAdventure
Go




