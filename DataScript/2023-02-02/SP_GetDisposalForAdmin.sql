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
CREATE PROCEDURE SP_GetDisposalForAdmin
 @DivisionCode nvarchar(50)=null,
 @TownshipCode nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
	  d.EmployeeCode
	,(select dt.DisposalType from TB_Disposal d INNER JOIN TB_DisposalType dt ON d.DisposalTypeCode=dt.DisposalTypeCode where d.EmployeeCode=e.EmployeeCode and d.IsDeleted=0) As 'DisposalType'
	,REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[DisposalDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉')  AS 'DisposalDate'
	,
	Remark
	,d.DisposalTypeCode
	,e.[Name]
	,d.IsDeleted
	,d.CreatedBy
	,d.CreatedDate
	,d.DisposalPkid,div.StateDivision,td.TownName As Township
	 from TB_Disposal d 
	 INNER JOIN TB_Employee e ON d.EmployeeCode=e.EmployeeCode
	  left join TB_CurrentJob_Township tw on e.TownshipCode=tw.TownshipCode
  left join TB_StateDivision div on div.StateDivisionCode=tw.StateDivisionID
  left join TB_TownAndDivision td on td.TownCode=tw.UploadForTownship
	  where
   (@DivisionCode is null or div.StateDivisionCode=@DivisionCode) AND
   (@TownshipCode is null or tw.TownshipCode=@TownshipCode) AND
	   d.IsDeleted=0 and e.IsDeleted=0
END
GO
