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
Alter PROCEDURE SP_GetLeaveEntitlementForAdmin
	 @DivisionCode nvarchar(50)=null,
	 @TownshipCode nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [LeaveEntitlementPkid]
	  , 
	  TB_LeaveEntitlement.[EmployeeCode]
	  , REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[ApprovedDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'ApprovedDate'
	  ,REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([ApprovedNo],'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'ApprovedNo'
	  , REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[StartDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'StartDate'
      , REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[EndDate],103),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'EndDate'
	  --, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(10),[Period]),'0',N'၀'),'1',N'၁'),'2',N'၂'),'3',N'၃'),'4',N'၄'),'5',N'၅'),'6',N'၆'),'7',N'၇'),'8',N'၈'),'9',N'၉') AS 'Period'
	  ,Period
	  ,TB_LeaveEntitlement.LeaveTypeCode
	  ,TB_LeaveEntitlement.[IsDeleted]
      ,TB_LeaveEntitlement.[ModifiedDate],TB_LeaveEntitlement.[CreatedDate],TB_LeaveEntitlement.[CreatedBy]
	  ,emp.[Name]
	  ,lt.LeaveType,div.StateDivision,td.TownName As Township
  FROM [TB_LeaveEntitlement] 
  join TB_Employee emp on emp.EmployeeCode=TB_LeaveEntitlement.EmployeeCode
  join TB_LeaveType lt on lt.LeaveTypeCode=TB_LeaveEntitlement.LeaveTypeCode
  left join TB_CurrentJob_Township tw on emp.TownshipCode=tw.TownshipCode
  left join TB_StateDivision div on div.StateDivisionCode=tw.StateDivisionID
  left join TB_TownAndDivision td on td.TownCode=tw.UploadForTownship

  WHERE 
   (@DivisionCode is null or div.StateDivisionCode=@DivisionCode) AND
   (@TownshipCode is null or tw.TownshipCode=@TownshipCode) AND
  TB_LeaveEntitlement.IsDeleted=0
	
END
GO
