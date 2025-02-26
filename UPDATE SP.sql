USE [Nissan]
GO
/****** Object:  StoredProcedure [dbo].[InsertTableSecurityByPlant]    Script Date: 3/8/2024 8:26:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertTableSecurityByPlant]
	@Plan_idFilter int ,@Category_idFilter int
AS
BEGIN
	
	SET NOCOUNT ON;

	 DECLARE @count INT
	 Declare @Plant_id INT
	 Declare @Order int
	 Set @Order = 0
	 SELECT @count = COUNT(Plant_id) FROM [Nissan].[dbo].[Plant] where active = 1
	 WHILE @count !=@Order 
	 BEGIN
	 set @Plant_id =  (SELECT [Plant_id] FROM [Nissan].[dbo].[Plant] where active = 1 ORDER BY [Plant_id] OFFSET @Order ROWS  FETCH NEXT 1 ROWS ONLY)
	
		DECLARE @count2 INT
		Declare @SubCategory_id INT
		Declare @Order2 int
		Set @Order2 = 0
		SELECT @count2 = COUNT(SubCategory_id) FROM [Nissan].[dbo].[SubCategory] where active = 1
		WHILE @count2 != @Order2
		BEGIN
		set @SubCategory_id =  (SELECT SubCategory_id FROM [Nissan].[dbo].[SubCategory] where active = 1 ORDER BY SubCategory_id OFFSET @Order2 ROWS  FETCH NEXT 1 ROWS ONLY)
			if not exists (SELECT [SubCategory_id], [Plant_id] FROM [Nissan].[dbo].[Security] where  [SubCategory_id] = @SubCategory_id  and [Plant_id] = @Plant_id)
				Insert into [Nissan].[dbo].[Security] ([SubCategory_id] ,[Plant_id],[Result],[Comment],[Active])
				SELECT @SubCategory_id SubCategory_id, @Plant_id Plant_id, 0 Result, '' Comment , 1 Active
		set @Order2 = @Order2 +1
		END
	set @Order = @Order +1
    END

if exists (
	 SELECT
	  F.Function_id
	 ,F.Function_description as Function_name
	 ,C.Category_name
	 ,C.Category_id
	 ,S.Plant_id
	 ,S.SubCategory_id
	 ,S.[Security_id]
		,C.[Category_description]
	  ,SC.SubCategory_description
      ,S.[Result]
      ,S.[Comment]
	  ,P.Plant_description
  FROM [Nissan].[dbo].[Security] S
  inner join [Nissan].[dbo].[SubCategory] SC on SC.SubCategory_id = S.SubCategory_id
  inner join [Nissan].[dbo].[Categories] C on C.Category_id = SC.Category_id
  inner join [Nissan].[dbo].[Function] F on F.Function_id = C.Function_id
  inner join [Nissan].[dbo].[Plant] P on P.Plant_id = S.Plant_id
  where S.active = 1 and SC.active = 1 and C.Active = 1 and P.Active = 1
  and C.Category_id = @Category_idFilter
  and S.Plant_id = @Plan_idFilter)

   SELECT  
    F.Function_id
	 ,F.Function_description as Function_name
	 ,C.Category_name
	 ,C.Category_id
	 ,S.Plant_id
	,S.SubCategory_id
   ,S.[Security_id]
		,C.[Category_description]
	  ,SC.SubCategory_description
      ,S.[Result]
      ,S.[Comment]
	  ,P.Plant_description
  FROM [Nissan].[dbo].[Security] S
  inner join [Nissan].[dbo].[SubCategory] SC on SC.SubCategory_id = S.SubCategory_id
  inner join [Nissan].[dbo].[Categories] C on C.Category_id = SC.Category_id
  inner join [Nissan].[dbo].[Function] F on F.Function_id = C.Function_id
  inner join [Nissan].[dbo].[Plant] P on P.Plant_id = S.Plant_id
  where S.active = 1 and SC.active = 1 and C.Active = 1 and F.Active = 1 and P.Active = 1
  and C.Category_id = @Category_idFilter
  and S.Plant_id = @Plan_idFilter
  else
  Select 3 as Category_id, 0, 0, 0, 0, 0 
END
