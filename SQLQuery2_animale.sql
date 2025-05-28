CREATE TABLE Animale (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nume NVARCHAR(50),
    Specie NVARCHAR(20), -- Pisica / Caine
    Varsta NVARCHAR(20),
    Descriere NVARCHAR(255),
    Imagine NVARCHAR(255) -- Path-ul imaginii
);
