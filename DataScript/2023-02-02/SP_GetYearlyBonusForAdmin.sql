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
CREATE PROCEDURE SP_GetYearlyBonusForAdmin
 @DivisionCode nvarchar(50)=null,
 @TownshipCode nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT YearlyBonusPkid
      ,TB_YearlyBonus.[EmployeeCode]
	  , REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[ApprovedDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'ApprovedDate'
	   , REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([ApprovedNo],'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'ApprovedNo'
	  ,[YearlyBonusCount]
	  , REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([YearlyBonusSalary],'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'YearlyBonusSalary'
	  , REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[YearlyBonusDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'YearlyBonusDate'
	  ,emp.[Name]
	  ,TB_YearlyBonus.IsDeleted
	  ,TB_YearlyBonus.CreatedDate
	  ,TB_YearlyBonus.CreatedBy,div.StateDivision,td.TownName As Township
  FROM [TB_YearlyBonus] join TB_Employee emp on emp.EmployeeCode=TB_YearlyBonus.EmployeeCode  
    left join TB_CurrentJob_Township tw on emp.TownshipCode=tw.TownshipCode
  left join TB_StateDivision div on div.StateDivisionCode=tw.StateDivisionID
  left join TB_TownAndDivision td on td.TownCode=tw.UploadForTownship

  WHERE
   (@DivisionCode is null or div.StateDivisionCode=@DivisionCode) AND
   (@TownshipCode is null or tw.TownshipCode=@TownshipCode) AND

   TB_YearlyBonus.IsDeleted=0 
END
GO
