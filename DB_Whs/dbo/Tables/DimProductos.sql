CREATE TABLE [dbo].[DimProductos] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [Codigo]                       VARCHAR (50)  NOT NULL,
    [Descripcion]                  VARCHAR (200) NULL,
    [Ultima_Modificacion]          DATETIME      NULL,
    [Ultima_Modificacion_Cantidad] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

