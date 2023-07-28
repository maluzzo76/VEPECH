CREATE PROCEDURE [dbo].[Sp_InsertUpdateDimDate]
AS
	INSERT INTO dbo.DimDate
	SELECT r1.codi, r1.Fecha
	FROM stg.Date r1 
	LEFT JOIN dbo.DimDate r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL
