CREATE TABLE dbo.AdoptieRequests (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Animal NVARCHAR(100),
    Prenume NVARCHAR(100),
    Nume NVARCHAR(100),
    Telefon NVARCHAR(100),
    Email NVARCHAR(100),
    Mesaj NVARCHAR(MAX),
    Status NVARCHAR(50),
    DataCerere DATETIME
)
