CREATE TABLE [dbo].[DimDate] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Codi]  VARCHAR (100) NOT NULL,
    [Fecha] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

