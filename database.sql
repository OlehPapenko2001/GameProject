create database gameProjectDB  

select * from users
use gameProjectDB

create table users(
userId int primary key,
userName varchar(20),
userAge int,
userPassword varchar(30))

alter proc proc_RegisterUser(@Name varchar(20), @Age int,@Id int out,@Password varchar(20) out)
as
begin
	if(select count(*) from  users)!=0
		set @Id = (select max(userId) from users)+1;
	else 
		set @Id	=101;	
	set @Password = CONCAT(@Name,@Age)
	insert into users 
	values (@Id, @Name, @Age, @Password);
end;

exec proc_RegisterUser 'admin', 123

create proc proc_Login(@Id int, @Password varchar(30), @UserName varchar(20) out)
as
begin
	if (select count(*) from users where userId=@Id and userPassword=@Password)=1
		set @UserName = (select userName from users where userId=@Id and userPassword=@Password);
	else
		set @UserName = null;
end;

--declare
--@Name varchar(20)
--begin
--	exec proc_Login 101, 'admin123', @Name out
--	print @Name
--end;
select* from users

----------------------------------------------------------------words table

create table words(
wordId int primary key,
word char(4))

drop table words

alter proc proc_AddWord(@Word varchar(20),@Id int out)
as
begin
	if(select COUNT(*) from words where word=@Word)=0
	begin;
		if(select count(*) from words)!=0
			set @Id = (select max(wordId) from words)+1;
		else 
			set @Id	=1;	
		insert into words 
		values (@Id, @Word);
	end;
	else
		set @Id = -1;	
end;

--exec proc_AddWord 'qwer'

alter proc proc_RemoveWord(@Id int)
as
begin
	delete from words where wordId=@Id
end;

--exec proc_RemoveWord 1

create proc proc_GetAllWords
as
begin
	select * from words
end;