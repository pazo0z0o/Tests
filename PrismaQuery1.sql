--Create Database PrismaTest;
USE PrismaTest;

Create Table Forms (
Id int Not Null IDENTITY(1,1) ,
Title varchar(100) Not NUll Unique,
Description varchar(200) NOT NULL,
DateOfCreation datetime2 NOT NUll,
LastUpdated datetime2
PRIMARY KEY (Id)
);

Create Table Fields (
Id int IDENTITY(1,1),
NoteName varchar(100) Not NUll Unique,
Note numeric NOT NULL,
FormId int,
PRIMARY KEY (Id),
FOREIGN KEY (FormId) REFERENCES Forms(Id),
);
--Apparently, I can't do subqueries in the alter table script, thus I had to learn 
GO
CREATE FUNCTION CountFieldsWithFormId(@FormId int)
RETURNS int
AS
BEGIN
    RETURN (SELECT COUNT(*) FROM Fields WHERE FormId = @FormId)
END
GO

-- add a check constraint to enforce the limit of at most 10 rows with the same FormId value in the Fields table
ALTER TABLE Fields
ADD CONSTRAINT chk_FormIdCount CHECK (
    FormId IS NULL OR dbo.CountFieldsWithFormId(FormId) <= 10
);