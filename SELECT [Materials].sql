SELECT [Materials].Mat_Arb_Name,[Materials].Mat_ID,[Materials].[Mat_Current_Quantity]
,[Units].Unit_Price1,[Units].Unit_Price2,[Units].[Unit_Name],
ISNULL(Mat_Unit_BarCode.BarCode,'') as BarCode1 FROM[Materials]
 join[Units] on [Materials].Mat_ID=[Units].Mat_ID 
left join Mat_Unit_BarCode on Mat_Unit_BarCode.Mat_ID=Materials.Mat_ID 
      
where [Units].[Unit_Name]='قطعة' OR [Units].[Unit_Name]='كيلو'

SELECT [Materials].Mat_Arb_Name,[Materials].Mat_ID,[Materials].[Mat_Current_Quantity]
,[Units].Unit_Price3,[Units].[Unit_Name] ,ISNULL(Mat_Unit_BarCode.BarCode,'') as BarCode1
 FROM[Materials] 
 join[Units] on [Materials].Mat_ID=[Units].Mat_ID
 left join Mat_Unit_BarCode on Mat_Unit_BarCode.Mat_ID=Materials.Mat_ID 
