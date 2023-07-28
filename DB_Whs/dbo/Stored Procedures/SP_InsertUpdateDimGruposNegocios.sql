
CREATE PROCEDURE [dbo].[SP_InsertUpdateDimGruposNegocios]
AS
INSERT INTO dbo.DimGruposNegocios
	SELECT r1.codi, r1.descripcion
	FROM stg.GruposNegocios r1 
	LEFT JOIN dbo.DimGruposNegocios r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL
