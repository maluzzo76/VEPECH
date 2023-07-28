CREATE PROCEDURE [dbo].[Sp_InsertUpdateDimRubros]
AS
	INSERT INTO dbo.DimRubros
	SELECT r1.codi, r1.Descripcion
	FROM stg.Rubro r1 
	LEFT JOIN dbo.DimRubros r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL




