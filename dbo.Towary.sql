CREATE TABLE [dbo].[Towary]
(
    [TowarId] INT IDENTITY NOT NULL PRIMARY KEY, 
    [Nazwa] NVARCHAR(50) NOT NULL, 
    [Cena] SMALLMONEY NOT NULL, 
    [Ilosc] INT NULL
)
