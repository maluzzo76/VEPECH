CREATE TABLE [stg].[Hoja_ruta] (
    [fecha]           DATETIME        NULL,
    [hoja_ruta_nume]  INT             NULL,
    [factu_nume]      INT             NULL,
    [transpor]        VARCHAR (200)   NULL,
    [cantidad_Lineas] INT             NULL,
    [rechazo]         VARCHAR (20)    NULL,
    [importeRechazo]  DECIMAL (18, 4) NULL,
    [importe]         DECIMAL (18, 4) NULL,
    [Cliente]         VARCHAR (200)   NULL
);

