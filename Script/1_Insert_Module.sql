USE [RDM_Report]
GO

INSERT INTO [dbo].[rdmsys_module]
           ([moduleid]
           ,[modulename_en]
           ,[modulename_th]
           ,[levelid]
           ,[parentid]
           ,[menuseq]
           ,[headerseq]
           ,[isheaderseq]
           ,[linkpage]
           ,[isactive])
     VALUES
           ('55000'
           ,'IRRBB'
           ,'IRRBB'
           ,'0'
           ,null
           ,null
           ,NULL
           ,1
           ,'IRRBB/IRRBB.aspx'
           ,1)
GO


