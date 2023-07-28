CREATE PROCEDURE [dbo].[SP_InsertUpdateDimProductos]
AS
INSERT INTO dbo.DimProductos
	SELECT r1.codigo, r1.descripcion,null,null
	FROM stg.Productos r1 
	LEFT JOIN dbo.DimProductos r2 ON r2.Codigo = r1.Codigo
	WHERE r2.Codigo IS NULL
