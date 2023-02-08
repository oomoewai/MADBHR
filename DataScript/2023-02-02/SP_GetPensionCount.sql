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
CREATE PROCEDURE SP_GetPensionCount
	@StateDivisionCode nvarchar(50)=null
AS
BEGIN
	Create Table #temp1 (StateDivisionCode nvarchar(50),TownshipCode nvarchar(50))
	Create Table #temp2 (StateDivision nvarchar(50),StateDivisionCode nvarchar(50),EmployeeCount int,TownshipCount int)
	Create Table #temp3 (StateDivisionCode nvarchar(50),TownshipCount int)

	SET NOCOUNT ON;
	insert into #temp1(StateDivisionCode,TownshipCode) 
	select div.StateDivisionCode,tw.TownshipCode from TB_Pension pen join 
	 TB_Employee emp on pen.EmployeeCode=emp.EmployeeCode
	  inner join  [TB_CurrentJob_Township] tw ON emp.TownshipCode=tw.TownshipCode 
	Left JOIN TB_JobHistory  jp ON emp.EmployeeCode =jp.EmployeeCode And jp.IsDeleted=0 AND jp.IsCurrent=1
	Left JOIN TB_RankType rt ON rt.RankTypeCode=jp.RankType_Code
	Left join TB_StateDivision div on div.StateDivisionCode=tw.StateDivisionID
	where (@StateDivisionCode is null or tw.StateDivisionID=@StateDivisionCode)
	  group by div.StateDivisionCode,tw.TownshipCode

	insert into #temp3(StateDivisionCode,TownshipCount)
	select StateDivisionCode,Count(TownshipCode) from #temp1 group by StateDivisionCode

	insert into #temp2 (StateDivision,StateDivisionCode,EmployeeCount)
	select div.StateDivision,div.StateDivisionCode, count(emp.EmployeeCode) from TB_Pension pen join 
	 TB_Employee emp on pen.EmployeeCode=emp.EmployeeCode
	inner join  [TB_CurrentJob_Township] tw ON emp.TownshipCode=tw.TownshipCode 
	Left JOIN TB_JobHistory  jp ON emp.EmployeeCode =jp.EmployeeCode And jp.IsDeleted=0 AND jp.IsCurrent=1
	Left JOIN TB_RankType rt ON rt.RankTypeCode=jp.RankType_Code
	Left join TB_StateDivision div on div.StateDivisionCode=tw.StateDivisionID
	where (@StateDivisionCode is null or tw.StateDivisionID=@StateDivisionCode)
	group by div.StateDivision,div.StateDivisionCode

	DECLARE 
    @divisionCode nvarchar(50) 
	DECLARE cursor_db CURSOR
	FOR SELECT 
        StateDivisionCode
    FROM #temp3;

	OPEN cursor_db;

	FETCH NEXT FROM cursor_db INTO 
		@divisionCode;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		update #temp2 set TownshipCount=(select TownshipCount from #temp3 where StateDivisionCode=@divisionCode) where StateDivisionCode=@divisionCode

    FETCH NEXT FROM cursor_db INTO 
        @divisionCode;
	END;

	CLOSE cursor_db;

	DEALLOCATE cursor_db;

	select * from #temp2

	drop table #temp1
	drop table #temp2
	drop table #temp3
END
GO
