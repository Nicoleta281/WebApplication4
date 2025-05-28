CREATE TABLE AdoptieRequests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Animal NVARCHAR(100),
    Nume NVARCHAR(100),
    Prenume NVARCHAR(100),
    Telefon NVARCHAR(20),
    Email NVARCHAR(100),
    Mesaj NVARCHAR(MAX),
    Status NVARCHAR(50),
    DataCerere DATETIME
);