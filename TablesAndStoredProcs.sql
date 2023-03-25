create database RoleBased;

CREATE TABLE Users(
	Id int Identity(1,1) PRIMARY KEY,
	Fullname varchar (200),
	Email varchar (100),
	Mobile varchar(30),
	Password varchar(150)
	);

select * from users

Alter Table Users add Role varchar(100); 

Update Users set Role='User' where Id = 1

Insert into Users values('Shravanthi','shravanthi2704@gmail.com','1234567890','Shravanthi@123');
-------Register----------------
GO
Create or ALTER procedure SP_Register
(
	@Fullname varchar(200),
	@Email varchar(200),
	@Mobile varchar(100),
	@Password varchar(100)
)
as
begin
IF (select Id from Users where Email=@Email) is not null 
	begin
		select 1;
	end
	else
	begin   
		Insert into Users (Fullname,Email,Mobile,Password)    
		Values (@Fullname,@Email,@Mobile,@Password) 
	end   
END

-------Login------------------
Go
create or ALTER procedure SP_Login
(
	@Email varchar(200),
	@Password varchar(100)
)
as
begin
select * from Users where Email=@Email and Password=@Password

END

-----------------Admin-----------------------------------
------create admin table-------------
CREATE TABLE AdminTabl(
	AdminId int Identity(1,1) PRIMARY KEY,
	AdminName varchar (200),
	AdminEmail varchar (100),
	AdminPassword varchar(150),
	AdminMobile varchar(150)
	);

Alter Table AdminTabl add Role varchar(100); 
select * from AdminTabl

Update AdminTabl set Role='Admin' where AdminId = 1

Insert into AdminTabl values('Shravanthi','shravanthi@gmail.com','Shravanthi@123','1234567890');

---------SP for AdminLogin-----------------
go
create or ALTER procedure [dbo].[SP_AdminLogin]
(
	@AdminEmail varchar(200),
	@AdminPassword varchar(100)
)
as
begin
select * from AdminTabl where AdminEmail=@AdminEmail and AdminPassword=@AdminPassword
END
------------------Product Table-------------------
CREATE TABLE ProductTable(
	ProductId int Identity(1,1) PRIMARY KEY,
	ProductName varchar (200),
	Image varchar (100),
	Description varchar(150),
	Rating varchar(150),
	Price int,
	AdminId int FOREIGN KEY (AdminId) REFERENCES AdminTabl(AdminId)
	);

Alter table ProductTable alter column Description varchar(500)

Select * from ProductTable;

Update ProductTable set Image='https://m.media-amazon.com/images/I/41rTqaJg9BL._AC_UF480,480_SR480,480_.jpg' where ProductId = 9;
-----------Add Product----------------
Go
Create or ALTER PROCEDURE Sp_AddProduct
@ProductName varchar(200),@Image varchar(500),@Description varchar(255),@Rating varchar(200),@Price int,@AdminId int

AS
BEGIN
insert into ProductTable(ProductName,Image,Description,Rating,Price,AdminId)
values(@ProductName,@Image,@Description,@Rating,@Price,@AdminId);
SELECT * from ProductTable
END

-------------Delete-------------
go
create or ALTER PROCEDURE [dbo].[Sp_Delete]
	@ProductId int
AS
BEGIN
delete from ProductTable where ProductId=@ProductId

	SELECT * from ProductTable
END

--------------Get All-------------------
go
create or ALTER procedure [dbo].[spGetAll]
as
begin
select * from ProductTable
END
----------------------------Get book By id---------------------
go
create or ALTER procedure [dbo].[Retrive_1_Product]
(
	@ProductId int
)
as
begin
select * from ProductTable where ProductId=@ProductId
END

----------------Update---------------------------
go
create or ALTER PROCEDURE [dbo].[Sp_UpdateProduct]
@ProductId int,
@ProductName varchar(200),@Image varchar(500),@Description varchar(255),@Rating varchar(200),@Price int
AS
BEGIN
update ProductTable set ProductName=@ProductName,Image=@Image,Description=@Description,Rating=@Rating,Price=@Price where ProductId=@ProductId
SELECT * from ProductTable
END

Insert into ProductTable values('HP Ultra Beautiful Laptop 348G4 Intel Core i5 Mobile-7th Gen -Processor','Image1.jpg','Processor type: core-7200u,Operating system: Windows 10 Pro,4 Nos. USB Port, 1 Nos. HDMI port,Feather Light 1.89KG','4',25999,1);
Insert into ProductTable values('Honor MagicBook 14','Image2.jpg','Processor type: core-7200u,Operating system: Windows 10 Pro,4 Nos. USB Port, 1 Nos. HDMI port,Feather Light 1.89KG','4',41999,1);
Insert into ProductTable values('Acer Extensa 15 Lightweight Laptop','Image3.jpg','Processor type: core-7200u,Operating system: Windows 10 Pro,4 Nos. USB Port, 1 Nos. HDMI port,Feather Light 1.89KG','5',34990,1);


----------Feedback Table---------------------------

create table FeedbackTable(
FeedbackId int Identity(1,1) PRIMARY KEY,
comment varchar(200),
Rating int,
Id int Foreign Key (Id) references Users(Id),
ProductId int FOREIGN KEY (ProductId) REFERENCES ProductTable(ProductId)
);

drop table FeedbackTable

select * from FeedbackTable
------Add Feedback----------
Go
Create or alter procedure Sp_AddFeedback1
(
@Id int,@ProductId int,@Comment varchar(max),@Rating int
)
as
Begin
Insert into FeedbackTable(comment,Rating,Id,ProductId) values (@Comment,@Rating,@Id,@ProductId);
End


---------------------------------------
Go
Create or alter procedure Sp_AddFeedback
(
@Id int,@ProductId int,@Comment varchar(max),@Rating int
)
as
Declare @AverageRating int;
Begin
	IF (EXISTS(SELECT * FROM FeedbackTable WHERE ProductId = @ProductId and Id=@Id))
		select 1; --already given feedback--
	Else
	Begin
		IF (EXISTS(SELECT * FROM ProductTable WHERE ProductId = @ProductId))
		Begin
			Begin try
				Begin transaction
					Insert into FeedbackTable values (@Id,@ProductId,@Comment,@Rating);		
					select @AverageRating=AVG(Rating) from FeedbackTable where ProductId = @ProductId;
					Update ProductTable set Rating=@AverageRating where ProductId = @ProductId;
				Commit Transaction
			End Try
			Begin catch
				Rollback transaction
			End catch
		End
		Else
		Begin
			Select 2; 
		End
	End
End

-------------------Get---join users table---------------------
go
Create or alter PROC spGetFeedback
	@ProductId INT
AS
BEGIN
	select 
		FeedbackTable.FeedbackId,FeedbackTable.Id,FeedbackTable.ProductId,FeedbackTable.Comment,FeedbackTable.Rating,Users.Fullname
		FROM Users
		inner join FeedbackTable
		on FeedbackTable.Id=Users.Id
		where ProductId=@ProductId
END
select * from FeedbackTable

-----------Get all feedback of Products-------------------
go
Create or alter PROC spGetFeedbacks
	@ProductId INT
AS
BEGIN
	select *
		FROM FeedbackTable
		where ProductId=@ProductId
END
select * from FeedbackTable

update FeedbackTable set Comment='Good Book' where FeedbackId = 3

----------PaymentTable----------------
create table PayTable
( PaymentId int Identity(1,1) PRIMARY KEY,
cardHolder varchar(100), 
cardNumber varchar(100), 
ExpiryDate varchar(100), 
CVV varchar(100) ) 

select * from PayTable

insert into PayTable(cardHolder,cardNumber,ExpiryDate,CVV) values ('shiva','1234567801231234','01/24','1234');