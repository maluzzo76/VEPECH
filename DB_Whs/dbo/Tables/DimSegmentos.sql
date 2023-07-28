CREATE TABLE [dbo].[DimSegmentos] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Codi]        VARCHAR (10)  NOT NULL,
    [Nombre]      VARCHAR (100) NULL,
    [Codi_orden3] VARCHAR (5)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

