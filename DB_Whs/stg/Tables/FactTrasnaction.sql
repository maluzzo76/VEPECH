CREATE TABLE [stg].[FactTrasnaction] (
    [TipoTransaccion]    VARCHAR (100)   NOT NULL,
    [Date_Id]            VARCHAR (100)   NOT NULL,
    [Producto_Codi]      VARCHAR (100)   NOT NULL,
    [Proveedor]          VARCHAR (100)   NULL,
    [Cliente]            VARCHAR (100)   NULL,
    [Vendedor]           VARCHAR (100)   NULL,
    [Supervisor]         VARCHAR (100)   NULL,
    [Segmanto]           VARCHAR (100)   NULL,
    [Rubro]              VARCHAR (100)   NULL,
    [GruposEstacionales] VARCHAR (100)   NULL,
    [GruposNegocios]     VARCHAR (100)   NULL,
    [Cantidad_KG]        DECIMAL (18, 4) NULL,
    [Cantidad_BU]        DECIMAL (18, 4) NULL,
    [Cantidad_LTS]       DECIMAL (18, 4) NULL,
    [Importe]            DECIMAL (18, 4) NULL,
    [Numero_Factura]     VARCHAR (20)    NULL
);

