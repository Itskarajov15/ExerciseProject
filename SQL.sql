CREATE DATABASE ApplicationDb;
USE ApplicationDB;

CREATE TABLE Users(
Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(50),
Password VARCHAR(256),
Salt VARBINARY(64)
);

CREATE TABLE Contragents(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(100) NOT NULL,
Address NVARCHAR(255) NOT NULL,
Mail VARCHAR(255),
VAT VARCHAR(11) NOT NULL,
UserId INT NOT NULL,

FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE FUNCTION GetContragentById(@contragentId INT)
RETURNS TABLE
AS
	RETURN (SELECT Id, Address, Mail, VAT, UserId 
			FROM Contragents
			WHERE Id = @contragentId);

GO;

CREATE PROC EditContragent(@id INT, @address NVARCHAR(255), @mail NVARCHAR(255), @vat NVARCHAR(11))
AS
	UPDATE Contragents
	SET Address = @address, Mail = @mail, VAT = @vat
	WHERE Id = @id;
GO