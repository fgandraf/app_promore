CREATE DATABASE [Promore]
GO

USE [Promore]
GO

-- DROP TABLE [Client]
-- DROP TABLE [Region]
-- DROP TABLE [Lot]
-- DROP TABLE [Professional]
-- DROP TABLE [User]

CREATE TABLE [User] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Role] INT,
    [Active] BIT,
    [Email] NVARCHAR(50) NOT NULL,
    [PasswordHash] VARCHAR(255) NOT NULL

    CONSTRAINT [PK_User] PRIMARY KEY([Id])
)

CREATE TABLE [Professional] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(100),
    [Cpf] NVARCHAR(11),
    [Profession] NVARCHAR(50),
    [UserId] INT NOT NULL

    CONSTRAINT [PK_Professional] PRIMARY KEY([Id]),
    CONSTRAINT [FK_Professional_User] FOREIGN KEY([UserId]) REFERENCES [User]([Id])
)

CREATE TABLE [Region] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(100) NOT NULL,
    [EstablishedDate] DATETIME,
    [StartDate] DATETIME,
    [EndDate] DATETIME

    CONSTRAINT [PK_Region] PRIMARY KEY([Id])
)

CREATE TABLE [Lot] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Block] INT NOT NULL,
    [Number] INT NOT NULL,
    [SurveyDate] DATETIME,
    [LastModifiedDate] DATETIME,
    [Status] BIT,
    [Comments] TEXT NOT NULL,
    [ProfessionalId] INT NOT NULL,
    [RegionId] INT NOT NULL

    CONSTRAINT [PK_Lot] PRIMARY KEY([Id]),
    CONSTRAINT [FK_Lot_Professional] FOREIGN KEY([ProfessionalId]) REFERENCES [Professional]([Id]),
    CONSTRAINT [FK_Lot_Region] FOREIGN KEY([RegionId]) REFERENCES [Region]([Id])
)

CREATE TABLE [Client] (
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Name] NVARCHAR(100),
    [Cpf] NVARCHAR(11),
    [Phone] NVARCHAR(11),
    [MothersName] NVARCHAR(100),
    [BirthdayDate] DATETIME,
    [LotId] INT NOT NULL

    CONSTRAINT [PK_Client] PRIMARY KEY([Id]),
    CONSTRAINT [FK_Client_Lot] FOREIGN KEY([LotId]) REFERENCES [Lot]([Id])
)