CREATE TABLE [dbo].[DimProveedores] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Codi]   VARCHAR (50)  NOT NULL,
    [Nombre] VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

