CREATE TABLE [dbo].[DimSupervisores] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Codi]   VARCHAR (10)  NOT NULL,
    [Nombre] VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

