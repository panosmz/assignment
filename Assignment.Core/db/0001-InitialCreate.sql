IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240309115722_InitialCreate'
)
BEGIN
    CREATE TABLE [Countries] (
        [Id] CHAR(3) NOT NULL,
        [CommonName] nvarchar(max) NOT NULL,
        [Capital] nvarchar(max) NOT NULL,
        [Borders] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240309115722_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240309115722_InitialCreate', N'8.0.2');
END;
GO

COMMIT;
GO

