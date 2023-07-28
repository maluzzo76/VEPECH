CREATE TABLE [dbo].[FactHojaRuta] (
    [id]                   INT             IDENTITY (1, 1) NOT NULL,
    [fecha]                DATETIME        NULL,
    [Numero_Hoja_Ruta]     INT             NULL,
    [Tipo_transporte]      VARCHAR (200)   NULL,
    [Transporista]         VARCHAR (100)   NULL,
    [Pedidos]              INT             NULL,
    [Anulados]             INT             NULL,
    [Parciales]            INT             NULL,
    [KM]                   INT             NULL,
    [Importe]              DECIMAL (18, 4) NULL,
    [Importe_Devoluciones] DECIMAL (18, 4) NULL,
    [Numero_de_vuelta]     INT             NULL,
    [Importe_sin_Iva]      DECIMAL (18, 4) NULL,
    [Costo_Flete]          DECIMAL (18, 4) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

