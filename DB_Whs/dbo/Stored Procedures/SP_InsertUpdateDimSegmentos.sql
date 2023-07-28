
CREATE PROCEDURE [dbo].[SP_InsertUpdateDimSegmentos]
AS
INSERT INTO dbo.DimSegmentos
	SELECT r1.codi, r1.nombre,r1.Codi_orden3
	FROM stg.segmentos r1 
	LEFT JOIN dbo.DimSegmentos r2 ON r2.Codi = r1.Codi
	WHERE r2.Codi IS NULL

-- Actualiza codi_orden3
update dimSegmentos set codi_orden3 = r1.codi_orden3
from(
select * from stg.segmentos
) r1
where dimsegmentos.codi = r1.Codi