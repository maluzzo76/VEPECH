﻿CREATE TABLE [dbo].[DimRubros] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Codi]   VARCHAR (5)   NOT NULL,
    [Nombre] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

