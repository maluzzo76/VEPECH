Use master
Go

Create procedure dbo.RestoreAmerica @kbkFileName varchar(max)
AS

ALTER DATABASE [America] set single_user with rollback immediate

RESTORE DATABASE [America] 
FROM  DISK = @kbkFileName
WITH  RESTRICTED_USER,  
FILE = 1, 
MOVE N'ame_test_datData' 
TO N'E:\MSSQL\MSSQL16.VEPECH_DB\MSSQL\DATA\\america.mdf',  
MOVE N'ame_test_log' 
TO N'E:\MSSQL\MSSQL16.VEPECH_DB\MSSQL\DATA\\america_1.ldf',  
NOUNLOAD,  REPLACE,  STATS = 5

ALTER DATABASE [America] SET MULTI_USER