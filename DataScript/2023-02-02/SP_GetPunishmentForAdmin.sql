-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE SP_GetPunishmentForAdmin
	-- Add the parameters for the stored procedure here
	@DivisionCode nvarchar(50)=null,
	@TownshipCode nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	 PunishmentPkid
	,p.[EmployeeCode]
	,[BranchCode]
	,[DepartmentCode]
	,[RankTypecode]
	,OrderDate As 'OrderDate_Eng'
	,REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[OrderDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'OrderDate'
	,REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([OrderNo],'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'OrderNo'
	--,OrderNo
	,REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(NVARCHAR(30),[CrimeYear],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'CrimeYear'
	--,(select pt.PunishmentType from TB_PunishmentType As pt,TB_Punishment As p where p.PunishmentTypeCode=pt.PunishmentTypeCode) AS 'PunishmentType'
	,pt.PunishmentType As 'PunishmentType'
	,p.[PunishmentTypeCode]
	,Description
	,p.[IsDeleted]
	,p.[ModifiedDate]
	,p.[CreatedDate]
	,p.[CreatedBy]
	,emp.[Name],div.StateDivision,td.TownName as Township
	FROM [dbo].[TB_Punishment] p
	INNER JOIN TB_PunishmentType pt ON pt.PunishmentTypeCode=p.PunishmentTypeCode
    join TB_Employee emp on emp.EmployeeCode=p.EmployeeCode
	left join TB_CurrentJob_Township tw on emp.TownshipCode=tw.TownshipCode
	left join TB_StateDivision div on div.StateDivisionCode=tw.StateDivisionID
	left join TB_TownAndDivision td on td.TownCode=tw.UploadForTownship
	WHERE
	(@DivisionCode is null or div.StateDivisionCode=@DivisionCode) AND
	(@TownshipCode is null or tw.TownshipCode=@TownshipCode) AND
	 p.IsDeleted=0 
END
GO
