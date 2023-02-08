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
Alter PROCEDURE Sp_GetTrainingHistoryForAdmin
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
	TrainingHistoryPkid
	,TB_TrainingHistory.[EmployeeCode]
	,[BranchCode]
	,[DepartmentCode]
	,[RankTypecode]
	,case when REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[StartDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') is null then '' else REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[StartDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') end AS 'StartDate'
	--,[StartDate]
	--,[EndDate]
	,case when REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[EndDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') is null then '' else REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[EndDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') end  AS 'EndDate'
	,[TrainingTitle]
	,[SchoolName]
	,[Location]
	,case when [Remark] is null then '' else [Remark] end As 'Remark'
	,TB_TrainingHistory.[IsDeleted]
	,TB_TrainingHistory.[ModifiedDate]
	,TB_TrainingHistory.[CreatedDate]
	,TB_TrainingHistory.[CreatedBy]
	,emp.[Name],div.StateDivision,td.TownName As Township
	FROM [dbo].[TB_TrainingHistory] join
	TB_Employee emp on emp.EmployeeCode=TB_TrainingHistory.EmployeeCode
	left join TB_CurrentJob_Township tw on emp.TownshipCode=tw.TownshipCode
	left join TB_StateDivision div on div.StateDivisionCode=tw.StateDivisionID
	left join TB_TownAndDivision td on td.TownCode=tw.UploadForTownship
	WHERE
	 (@DivisionCode is null or div.StateDivisionCode=@DivisionCode) AND
	 (@TownshipCode is null or tw.TownshipCode=@TownshipCode) AND
	 TB_TrainingHistory.IsDeleted=0  
	 order by TB_TrainingHistory.CreatedDate DESC
END
GO
