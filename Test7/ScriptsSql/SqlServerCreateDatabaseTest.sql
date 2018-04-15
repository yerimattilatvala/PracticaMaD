/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */


 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\SourceCode\DataBase\'
 
USE [master]


/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'madDatabase_test')
DROP DATABASE [madDatabase_test]


USE [master]


/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [madDatabase_test] 
    ON PRIMARY ( NAME = madDatabase_test, FILENAME = "' + @Default_DB_Path + N'madDatabase_test.mdf")
    LOG ON ( NAME = madDatabase_log_test, FILENAME = "' + @Default_DB_Path + N'madDatabase_test_log.ldf")'

EXEC(@sql)
PRINT N'Database [MaDDatabase_test] created.'
GO

USE [madDatabase_test]


/* ********** Drop Table UserProfile if already exists *********** */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CD]') AND type in ('U'))
DROP TABLE [CD]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Movie]') AND type in ('U'))
DROP TABLE [Movie]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Book]') AND type in ('U'))
DROP TABLE [Book]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[OrderLine]') AND type in ('U'))
DROP TABLE [OrderLine]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Orders]') AND type in ('U'))
DROP TABLE [Orders]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Product]') AND type in ('U'))
DROP TABLE [Product]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Card]') AND type in ('U'))
DROP TABLE [Card]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO

/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */
PRINT N'ENTRADA'
CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,
	postalAddress int NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName),
	CONSTRAINT [UniqueKey_Email] UNIQUE (email)
)

PRINT N'CREA USUARIO'

CREATE TABLE Card
(
    cardNumber int NOT NULL,
	usrId bigint NOT NULL,
    verificationCode int NOT NULL,
	expirationDate datetime2 NOT NULL,
	cardType varchar(10) NOT NULL,
	defaultCard bit NOT NULL,

	CONSTRAINT [PK_Card] PRIMARY KEY (cardNumber),
	CONSTRAINT [FK_CardusrId] FOREIGN KEY (usrId)
		REFERENCES UserProfile (usrId) ON DELETE CASCADE
)
PRINT N'CREA CARD'

CREATE TABLE Category
(
	categoryId bigint IDENTITY(1,1) NOT NULL,
	name varchar(10) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId)
)
PRINT N'CREA category'

CREATE TABLE Product
(
    productId bigint IDENTITY(1,1) NOT NULL,
	categoryId bigint NOT NULL,
    name varchar(10) NOT NULL,
	registerDate datetime2 NOT NULL,
	numberOfUnits int NOT NULL,
	prize float NOT NULL,

	CONSTRAINT [PK_Product] PRIMARY KEY (productId),
	CONSTRAINT [FK_ProductcategoryId] FOREIGN KEY (categoryId)
		REFERENCES Category (categoryId)
)
PRINT N'CREA PRODUCT'

CREATE TABLE Orders
(
	orderId bigint IDENTITY(1,1) NOT NULL,
	orderDate datetime2 NOT NULL,
	usrId bigint NOT NULL,
	cardNumber int NOT NULL,
	postalAddress int NOT NULL,
	
	CONSTRAINT [PK_Orders] PRIMARY KEY (orderId),
	CONSTRAINT [FK_OrderuserdId] FOREIGN KEY (usrId)
		REFERENCES UserProfile (usrId),
	CONSTRAINT [FK_OrdercardNumber] FOREIGN KEY (cardNumber)
		REFERENCES Card (cardNumber)
)
PRINT N'CREA order'
CREATE TABLE OrderLine
(
	numberId bigint IDENTITY(1,1) NOT NULL,
	orderId bigint NOT NULL,
	productId bigint NOT NULL,
	numberOfUnits int NOT NULL, 
	unitPrize float NOT NULL,

	CONSTRAINT [PK_OrderLine] PRIMARY KEY (numberId),
	CONSTRAINT [FK_OrderLineorderId] FOREIGN KEY (orderId)
		REFERENCES Orders (orderId),
	CONSTRAINT [FK_OrderLineproductId] FOREIGN KEY (productId)
		REFERENCES Product (productId)
)
PRINT N'CREA orderline'
CREATE TABLE Book
(
	productId bigint NOT NULL,
	title varchar(20) NOT NULL,
	author varchar(20) NOT NULL,
	summary varchar(50) NOT NULL,
	topic varchar(20) NOT NULL,
	pages int NOT NULL,

	CONSTRAINT [PK_BookproductId] PRIMARY KEY (productId),
	CONSTRAINT [FK_BookproductId] FOREIGN KEY (productId)
		REFERENCES Product (productId)
)
PRINT N'CREA book'
CREATE TABLE CD
(
	productId bigint NOT NULL,
	title varchar(20) NOT NULL,
	artist varchar(20) NOT NULL,
	topic varchar (10) NOT NULL,
	songs int  NOT NULL,

	CONSTRAINT [PK_CDproductId] PRIMARY KEY (productId),
	CONSTRAINT [FK_CDproductId] FOREIGN KEY (productId)
		REFERENCES Product (productId)
)

CREATE TABLE Movie
(
	productId bigint NOT NULL,
	title varchar(20) NOT NULL,
	director varchar(20) NOT NULL,
	summary varchar(50) NOT NULL,
	topic varchar(20) NOT NULL,
	duration int NOT NULL,

	CONSTRAINT [PK_MovieproductId] PRIMARY KEY (productId),
	CONSTRAINT [FK_MovieproductId] FOREIGN KEY (productId)
		REFERENCES Product (productId)
)

CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO

GO

