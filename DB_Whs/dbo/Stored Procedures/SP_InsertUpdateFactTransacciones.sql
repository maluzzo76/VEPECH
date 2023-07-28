CREATE PROCEDURE [dbo].[SP_InsertUpdateFactTransacciones]
AS
TRUNCATE TABLE dbo.FactTransacciones

INSERT INTO dbo.FactTransacciones
SELECT 
tt.id TipoTransacciones_id,
(SELECT MAX(d.Id) FROM DimDate d WHERE d.Codi = ft1.Date_Id ),
p.id producto_id,
null proveedor,
cli.id,
ven.Id,
sup.id,
seg.id,
ru.id,
ge.id,
gn.id,
ft1.Cantidad_KG,
ft1.Cantidad_BU,
ft1.Cantidad_LTS,
ft1.Importe,
ft1.Numero_Factura

FROM stg.FactTrasnaction ft1
INNER JOIN DimTiposTransacciones tt ON tt.Nombre = ft1.TipoTransaccion 
INNER JOIN DimProductos p ON p.Codigo = ft1.Producto_Codi 
INNER JOIN DimClientes cli ON cli.Codi = ft1.Cliente
INNER JOIN DimVendedores ven ON ven.Codi = ft1.Vendedor 
INNER JOIN DimSupervisores sup ON sup.Codi = ft1.Supervisor
INNER JOIN DimSegmentos seg ON seg.Codi = ft1.Segmanto 
INNER JOIN DimRubros ru ON ru.Codi = ft1.Rubro
INNER JOIN DimGruposEstacionales ge ON ge.Codi = ft1.GruposEstacionales 
INNER JOIN DimGruposNegocios gn ON gn.Codi = ft1.GruposNegocios
where  ft1.TipoTransaccion = 'VENTA'



INSERT INTO dbo.FactTransacciones
SELECT 
tt.id TipoTransacciones_id,
(SELECT MAX(d.Id) FROM DimDate d WHERE d.Codi = ft1.Date_Id ),
p.id producto_id,
pro.id proveedor,
null,
null,
null,
seg.id,
ru.id,
ge.id,
gn.id,
ft1.Cantidad_KG,
ft1.Cantidad_BU,
ft1.Cantidad_LTS,
ft1.Importe,
ft1.Numero_Factura

FROM stg.FactTrasnaction ft1
INNER JOIN DimTiposTransacciones tt ON tt.Nombre = ft1.TipoTransaccion 
INNER JOIN DimProductos p ON p.Codigo = ft1.Producto_Codi 
INNER JOIN DimProveedores pro on pro.Codi = ft1.proveedor


INNER JOIN DimSegmentos seg ON seg.Codi = ft1.Segmanto 
INNER JOIN DimRubros ru ON ru.Codi = ft1.Rubro
INNER JOIN DimGruposEstacionales ge ON ge.Codi = ft1.GruposEstacionales 
INNER JOIN DimGruposNegocios gn ON gn.Codi = ft1.GruposNegocios
where  ft1.TipoTransaccion = 'COMPRA'

