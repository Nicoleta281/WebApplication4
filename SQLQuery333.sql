IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PostariBlog' AND xtype='U')
BEGIN
    CREATE TABLE PostariBlog (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titlu NVARCHAR(280) NOT NULL,
    Continut NVARCHAR(MAX) NOT NULL,
    Imagine NVARCHAR(300),
    Data DATETIME NOT NULL
    )
END

