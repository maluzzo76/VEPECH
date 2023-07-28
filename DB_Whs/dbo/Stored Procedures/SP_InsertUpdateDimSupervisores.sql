
CREATE PROCEDURE [dbo].[SP_InsertUpdateDimSupervisores]
AS
INSERT INTO dbo.DimSupervisores
	SELECT r1.codi, r1.nombre
	FROM stg.Supervisores r1 
	LEFT JOIN dbo.DimSupervisores r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL
