
CREATE PROCEDURE [dbo].[SP_InsertUpdateDimVendedores]
AS
INSERT INTO dbo.DimVendedores
	SELECT r1.codi, r1.nombre
	FROM stg.Vendedores r1 
	LEFT JOIN dbo.DimVendedores r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL
