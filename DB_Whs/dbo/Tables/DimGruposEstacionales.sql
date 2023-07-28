CREATE TABLE [dbo].[DimGruposEstacionales] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Codi]        VARCHAR (4)   NOT NULL,
    [Descripcion] VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

