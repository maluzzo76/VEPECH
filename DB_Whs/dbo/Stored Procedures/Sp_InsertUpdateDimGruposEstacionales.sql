CREATE PROCEDURE [dbo].[Sp_InsertUpdateDimGruposEstacionales]
AS
INSERT INTO dbo.DimGruposEstacionales
	SELECT r1.codi, r1.descripcion
	FROM stg.GruposEstacionales r1 
	LEFT JOIN dbo.DimGruposEstacionales r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL
