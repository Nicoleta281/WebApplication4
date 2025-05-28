CREATE TABLE [dbo].[UDTables] (
    [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [Username] NVARCHAR(30) NOT NULL,
    [Password] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(30) NOT NULL,
    [LastLogin] DATETIME NULL,
    [Level] INT NOT NULL
);