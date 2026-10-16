IF DB_ID('BooksDemo') IS NULL
BEGIN
    CREATE DATABASE BooksDemo;
END;
GO

USE BooksDemo;
GO

IF OBJECT_ID('dbo.Books', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Books
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Books PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Author NVARCHAR(150) NOT NULL,
        PublicationYear INT NOT NULL
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Books)
BEGIN
    INSERT INTO dbo.Books (Title, Author, PublicationYear)
    VALUES
        (N'Don Quijote de la Mancha', N'Miguel de Cervantes', 1605),
        (N'Cien años de soledad', N'Gabriel García Márquez', 1967),
        (N'El principito', N'Antoine de Saint-Exupéry', 1943);
END;
GO
