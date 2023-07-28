
CREATE PROCEDURE [dbo].[SP_InsertUpdateDimProveedores]
AS
INSERT INTO dbo.DimProveedores
	SELECT r1.codi, r1.Nombre
	FROM stg.Proveedores r1 
	LEFT JOIN dbo.DimProveedores r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL
