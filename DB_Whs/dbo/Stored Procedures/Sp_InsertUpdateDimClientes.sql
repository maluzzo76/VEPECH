CREATE PROCEDURE [dbo].[Sp_InsertUpdateDimClientes]
AS
	INSERT INTO dbo.DimClientes
	SELECT r1.codi, r1.nombre, r1.razon_social
	FROM stg.Clientes r1 
	LEFT JOIN dbo.DimClientes r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL