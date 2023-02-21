using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using MADBHR_Models.Employee;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.SqlDataAccess
{
    public class EmployeeDAO : GenericCommonExtensions<TbEmployee>
    {

        public dynamic SaveEmployee(IDbCommand cmd, TbEmployee employee, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_Employee_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@EmployeePkid", Id);
                cmd.AddParameter("@EmployeeCode", employee.EmployeeCode);
                cmd.AddParameter("@SerialNumber", employee.SerialNumber);
                cmd.AddParameter("@Name", employee.Name);
                cmd.AddParameter("@FatherName", employee.FatherName);
                cmd.AddParameter("@MotherName", employee.MotherName);
                cmd.AddParameter("@Gender", employee.Gender);
                cmd.AddParameter("@Religion", employee.Religion);
                cmd.AddParameter("@Race", employee.Race);
                cmd.AddParameter("@NRCNumber", employee.Nrcnumber);
                cmd.AddParameter("@PlaceOfBirth", employee.PlaceOfBirth);
                cmd.AddParameter("@Education", employee.EducationTypeCode);
                cmd.AddParameter("@Occupation", employee.Occupation);
                cmd.AddParameter("@DateOfBirth", employee.DateOfBirth);
                cmd.AddParameter("@EyeColor", employee.EyeColor);
                cmd.AddParameter("@Height", employee.Height);
                cmd.AddParameter("@Mark", employee.Mark);
                cmd.AddParameter("@OtherName", employee.OtherName);
                cmd.AddParameter("@JoinDate", employee.JoinDate);
                cmd.AddParameter("@TownshipCode", employee.Occupation);
                cmd.AddParameter("@Address", employee.Address);
                cmd.AddParameter("@DearestPerson", employee.DearestPerson);
                cmd.AddParameter("@Ancestor", employee.Ancestor);
                cmd.AddParameter("@IsActive", "Active");
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@IsRecordEdited", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", employee.UploadForTownship);
                cmd.AddParameter("@ProfilePic", employee.ProfilePic);
                cmd.AddParameter("@NrcPic", employee.Nrcpic);
                cmd.AddParameter("@Form66Pic", employee.Form66Pic);
                cmd.AddParameter("@Status", employee.Status);
                cmd.AddParameter("@RejectComment", employee.RejectComment);
                cmd.AddParameter("@EditStatus", employee.EditRequest);
                cmd.AddParameter("@EditComment", employee.EditComment);
                cmd.AddParameter("@DeleteStatus", employee.DeleteRequest);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return employee;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbEmployee> GetEmployee(IDbCommand cmd, string? Name = null, DateTime? FromDate = null, DateTime? ToDate = null,string? SerialNumber=null)
        {

            cmd.CommandText = "Sp_Employee_Select";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@Name", Name);
            cmd.AddParameter("@FromDate", FromDate);
            cmd.AddParameter("@ToDate", ToDate);
            cmd.AddParameter("@SerialNumber", SerialNumber);
           
            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbEmployee> emps = new List<TbEmployee>();
            if (ResDs != null)
            {
                if (ResDs.Tables.Count > 0)
                {
                    if (ResDs.Tables[0] != null)
                    {
                        if (ResDs.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ResDs.Tables[0].Rows.Count; i++)
                            {
                                TbEmployee employee =new TbEmployee{
                                    EmployeePkid = ResDs.Tables[0].Rows[i]["EmployeePkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["EmployeePkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    CurrentRank = ResDs.Tables[0].Rows[i]["CurrentRank"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CurrentRank"].ToString() : "",
                                    SerialNumber = ResDs.Tables[0].Rows[i]["SerialNumber"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SerialNumber"].ToString() : "",
                                    SerialNumber_Myan = ResDs.Tables[0].Rows[i]["SerialNumber_Myan"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SerialNumber_Myan"].ToString() : "",
                                    Name = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    FatherName = ResDs.Tables[0].Rows[i]["Father_Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Father_Name"].ToString() : "",
                                    MotherName = ResDs.Tables[0].Rows[i]["Mother_Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Mother_Name"].ToString() : "",
                                    Gender = ResDs.Tables[0].Rows[i]["Gender"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Gender"].ToString() : "",
                                    Religion = ResDs.Tables[0].Rows[i]["Religion"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Religion"].ToString() : "",
                                    Race = ResDs.Tables[0].Rows[i]["Race"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Race"].ToString() : "",
                                    Nrcnumber = ResDs.Tables[0].Rows[i]["Nrcnumber"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Nrcnumber"].ToString() : "",
                                    PlaceOfBirth = ResDs.Tables[0].Rows[i]["PlaceOfBirth"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PlaceOfBirth"].ToString() : "",
                                    EducationType = ResDs.Tables[0].Rows[i]["EducationType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EducationType"].ToString() : "",
                                    EducationTypeCode = ResDs.Tables[0].Rows[i]["EducationTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EducationTypeCode"].ToString() : "",
                                    PlaceOfBirthName = ResDs.Tables[0].Rows[i]["PlaceOfBirthName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PlaceOfBirthName"].ToString() : "",
                                    Occupation = ResDs.Tables[0].Rows[i]["Occupation"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Occupation"].ToString() : "",
                                    DateOfBirthString = ResDs.Tables[0].Rows[i]["DateOfBirth"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DateOfBirth"].ToString() : "",
                                    EyeColor = ResDs.Tables[0].Rows[i]["EyeColor"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EyeColor"].ToString() : "",
                                    Height = ResDs.Tables[0].Rows[i]["Height"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Height"].ToString() : "",
                                    Mark = ResDs.Tables[0].Rows[i]["Mark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Mark"].ToString() : "",
                                    OtherName = ResDs.Tables[0].Rows[i]["OtherName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OtherName"].ToString() : "",
                                    JoinDateString = ResDs.Tables[0].Rows[i]["JoinDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["JoinDate"].ToString() : "",
                                    CurrentRankDate = ResDs.Tables[0].Rows[i]["CurrentRankDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CurrentRankDate"].ToString() : "",
                                    TownshipCode = ResDs.Tables[0].Rows[i]["TownshipCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TownshipCode"].ToString() : "",
                                    Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                    Age = ResDs.Tables[0].Rows[i]["Age"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["Age"]) : 0,
                                    Address = ResDs.Tables[0].Rows[i]["Address"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Address"].ToString() : "",
                                    DearestPerson = ResDs.Tables[0].Rows[i]["DearestPerson"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DearestPerson"].ToString() : "",
                                    Ancestor = ResDs.Tables[0].Rows[i]["Ancestor"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Ancestor"].ToString() : "",
                                    IsActive = ResDs.Tables[0].Rows[i]["IsActive"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["IsActive"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    IsRecordEdited = ResDs.Tables[0].Rows[i]["IsRecordEdited"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsRecordEdited"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                emps.Add(employee);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return emps;

        }

        public List<TbEmployee> GetRequestingEmployee(IDbCommand cmd)
        {

            cmd.CommandText = "SP_GetRequestingEmployee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
           

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbEmployee> emps = new List<TbEmployee>();
            if (ResDs != null)
            {
                if (ResDs.Tables.Count > 0)
                {
                    if (ResDs.Tables[0] != null)
                    {
                        if (ResDs.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ResDs.Tables[0].Rows.Count; i++)
                            {
                                TbEmployee employee = new TbEmployee
                                {
                                    EmployeePkid = ResDs.Tables[0].Rows[i]["EmployeePkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["EmployeePkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    CurrentRank = ResDs.Tables[0].Rows[i]["CurrentRank"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CurrentRank"].ToString() : "",
                                    SerialNumber = ResDs.Tables[0].Rows[i]["SerialNumber"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SerialNumber"].ToString() : "",
                                    SerialNumber_Myan = ResDs.Tables[0].Rows[i]["SerialNumber_Myan"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SerialNumber_Myan"].ToString() : "",
                                    Name = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    FatherName = ResDs.Tables[0].Rows[i]["Father_Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Father_Name"].ToString() : "",
                                    MotherName = ResDs.Tables[0].Rows[i]["Mother_Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Mother_Name"].ToString() : "",
                                    Gender = ResDs.Tables[0].Rows[i]["Gender"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Gender"].ToString() : "",
                                    Religion = ResDs.Tables[0].Rows[i]["Religion"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Religion"].ToString() : "",
                                    Race = ResDs.Tables[0].Rows[i]["Race"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Race"].ToString() : "",
                                    Nrcnumber = ResDs.Tables[0].Rows[i]["Nrcnumber"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Nrcnumber"].ToString() : "",
                                    PlaceOfBirth = ResDs.Tables[0].Rows[i]["PlaceOfBirth"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PlaceOfBirth"].ToString() : "",
                                    EducationType = ResDs.Tables[0].Rows[i]["EducationType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EducationType"].ToString() : "",
                                    EducationTypeCode = ResDs.Tables[0].Rows[i]["EducationTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EducationTypeCode"].ToString() : "",
                                    PlaceOfBirthName = ResDs.Tables[0].Rows[i]["PlaceOfBirthName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PlaceOfBirthName"].ToString() : "",
                                    Occupation = ResDs.Tables[0].Rows[i]["Occupation"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Occupation"].ToString() : "",
                                    DateOfBirthString = ResDs.Tables[0].Rows[i]["DateOfBirth"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DateOfBirth"].ToString() : "",
                                    EyeColor = ResDs.Tables[0].Rows[i]["EyeColor"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EyeColor"].ToString() : "",
                                    Height = ResDs.Tables[0].Rows[i]["Height"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Height"].ToString() : "",
                                    Mark = ResDs.Tables[0].Rows[i]["Mark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Mark"].ToString() : "",
                                    OtherName = ResDs.Tables[0].Rows[i]["OtherName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OtherName"].ToString() : "",
                                    JoinDateString = ResDs.Tables[0].Rows[i]["JoinDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["JoinDate"].ToString() : "",
                                    CurrentRankDate = ResDs.Tables[0].Rows[i]["CurrentRankDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CurrentRankDate"].ToString() : "",
                                    TownshipCode = ResDs.Tables[0].Rows[i]["TownshipCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TownshipCode"].ToString() : "",
                                    Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                    Age = ResDs.Tables[0].Rows[i]["Age"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["Age"]) : 0,
                                    Address = ResDs.Tables[0].Rows[i]["Address"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Address"].ToString() : "",
                                    DearestPerson = ResDs.Tables[0].Rows[i]["DearestPerson"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DearestPerson"].ToString() : "",
                                    Ancestor = ResDs.Tables[0].Rows[i]["Ancestor"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Ancestor"].ToString() : "",
                                    IsActive = ResDs.Tables[0].Rows[i]["IsActive"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["IsActive"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    IsRecordEdited = ResDs.Tables[0].Rows[i]["IsRecordEdited"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsRecordEdited"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0,
                                    EditRequest = ResDs.Tables[0].Rows[i]["EditRequest"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EditRequest"].ToString() : "",
                                    EditComment= ResDs.Tables[0].Rows[i]["EditComment"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EditComment"].ToString() : "",
                                    DeleteRequest = ResDs.Tables[0].Rows[i]["DeleteRequest"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DeleteRequest"].ToString() : "",

                                };

                                emps.Add(employee);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return emps;

        }
        public List<VMEmployeeCount> GetEmployeeCounts(IDbCommand cmd,string? StateDivisionCode=null)
        {
            cmd.CommandText = "SP_GetEmpCountByStateDivision";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@StateDivisionCode", StateDivisionCode);
            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<VMEmployeeCount> emps = new List<VMEmployeeCount>();
            if (ResDs != null)
            {
                if (ResDs.Tables.Count > 0)
                {
                    if (ResDs.Tables[0] != null)
                    {
                        if (ResDs.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ResDs.Tables[0].Rows.Count; i++)
                            {
                                VMEmployeeCount employee = new VMEmployeeCount();

                                employee.StateDivisionCode = ResDs.Tables[0].Rows[i]["StateDivisionCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivisionCode"].ToString() : "";
                                employee.StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "";
                                employee.TownshipCount = ResDs.Tables[0].Rows[i]["TownshipCount"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TownshipCount"]) : 0;
                                employee.EmployeeCount = ResDs.Tables[0].Rows[i]["EmployeeCount"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["EmployeeCount"]) : 0;
                                employee.TotalUpperRank= ResDs.Tables[0].Rows[i]["UpperRankCount"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["UpperRankCount"]) : 0;
                                employee.TotalLowerRank = ResDs.Tables[0].Rows[i]["LowerRankCount"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["LowerRankCount"]) : 0;

                                emps.Add(employee);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return emps;

        }

        public List<TbEmployee> GetEmployeeForAdmin(IDbCommand cmd, string? SateDivisionCode=null,string? TownshipCode=null, string? Status = null)
        {

            cmd.CommandText = "SP_GetEmployeeForAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@DivisionCode", SateDivisionCode);
            cmd.AddParameter("@TownshipCode", TownshipCode);
            cmd.AddParameter("@Status", Status);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbEmployee> emps = new List<TbEmployee>();
            if (ResDs != null)
            {
                if (ResDs.Tables.Count > 0)
                {
                    if (ResDs.Tables[0] != null)
                    {
                        if (ResDs.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ResDs.Tables[0].Rows.Count; i++)
                            {
                                TbEmployee employee = new TbEmployee
                                {
                                    EmployeePkid = ResDs.Tables[0].Rows[i]["EmployeePkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["EmployeePkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    CurrentRank = ResDs.Tables[0].Rows[i]["CurrentRank"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CurrentRank"].ToString() : "",
                                    SerialNumber = ResDs.Tables[0].Rows[i]["SerialNumber"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SerialNumber"].ToString() : "",
                                    SerialNumber_Myan = ResDs.Tables[0].Rows[i]["SerialNumber_Myan"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SerialNumber_Myan"].ToString() : "",
                                    Name = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    FatherName = ResDs.Tables[0].Rows[i]["Father_Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Father_Name"].ToString() : "",
                                    MotherName = ResDs.Tables[0].Rows[i]["Mother_Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Mother_Name"].ToString() : "",
                                    Gender = ResDs.Tables[0].Rows[i]["Gender"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Gender"].ToString() : "",
                                    Religion = ResDs.Tables[0].Rows[i]["Religion"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Religion"].ToString() : "",
                                    Race = ResDs.Tables[0].Rows[i]["Race"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Race"].ToString() : "",
                                    Nrcnumber = ResDs.Tables[0].Rows[i]["Nrcnumber"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Nrcnumber"].ToString() : "",
                                    PlaceOfBirth = ResDs.Tables[0].Rows[i]["PlaceOfBirth"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PlaceOfBirth"].ToString() : "",
                                    EducationType = ResDs.Tables[0].Rows[i]["EducationType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EducationType"].ToString() : "",
                                    EducationTypeCode = ResDs.Tables[0].Rows[i]["EducationTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EducationTypeCode"].ToString() : "",
                                    PlaceOfBirthName = ResDs.Tables[0].Rows[i]["PlaceOfBirthName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PlaceOfBirthName"].ToString() : "",
                                    Occupation = ResDs.Tables[0].Rows[i]["Occupation"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Occupation"].ToString() : "",
                                    OccupationName = ResDs.Tables[0].Rows[i]["OccupationName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OccupationName"].ToString() : "",
                                    DateOfBirthString = ResDs.Tables[0].Rows[i]["DateOfBirth"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DateOfBirth"].ToString() : "",
                                    EyeColor = ResDs.Tables[0].Rows[i]["EyeColor"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EyeColor"].ToString() : "",
                                    Height = ResDs.Tables[0].Rows[i]["Height"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Height"].ToString() : "",
                                    Mark = ResDs.Tables[0].Rows[i]["Mark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Mark"].ToString() : "",
                                    OtherName = ResDs.Tables[0].Rows[i]["OtherName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OtherName"].ToString() : "",
                                    JoinDateString = ResDs.Tables[0].Rows[i]["JoinDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["JoinDate"].ToString() : "",
                                    CurrentRankDate = ResDs.Tables[0].Rows[i]["CurrentRankDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CurrentRankDate"].ToString() : "",
                                    TownshipCode = ResDs.Tables[0].Rows[i]["TownshipCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TownshipCode"].ToString() : "",
                                    StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                    Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                    Age = ResDs.Tables[0].Rows[i]["Age"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["Age"]) : 0,
                                    Address = ResDs.Tables[0].Rows[i]["Address"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Address"].ToString() : "",
                                    DearestPerson = ResDs.Tables[0].Rows[i]["DearestPerson"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DearestPerson"].ToString() : "",
                                    Ancestor = ResDs.Tables[0].Rows[i]["Ancestor"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Ancestor"].ToString() : "",
                                    IsActive = ResDs.Tables[0].Rows[i]["IsActive"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["IsActive"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    IsRecordEdited = ResDs.Tables[0].Rows[i]["IsRecordEdited"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsRecordEdited"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0,
                                    Status = ResDs.Tables[0].Rows[i]["Status"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Status"].ToString() : "",
                                    RejectComment = ResDs.Tables[0].Rows[i]["RejectComment"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RejectComment"].ToString() : "",
                                    EditRequest = ResDs.Tables[0].Rows[i]["EditRequest"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EditRequest"].ToString() : "",
                                    EditComment = ResDs.Tables[0].Rows[i]["EditComment"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EditComment"].ToString() : "",
                                    DeleteRequest = ResDs.Tables[0].Rows[i]["DeleteRequest"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DeleteRequest"].ToString() : ""
                                };

                                emps.Add(employee);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return emps;

        }

        public void DeleteEmployee(IDbCommand cmd,string EmployeeCode,int userId)
        {
            
            cmd.CommandText = "Sp_Employee_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@EmployeeCode", EmployeeCode);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbEmployee aInfo, string prefix)
        {
            if (prefix == "GetAll")
            {
                aInfo = new TbEmployee()
                {
                    EmployeePkid = aDataReader["EmployeePkid"] != DBNull.Value ? Convert.ToInt32(aDataReader["EmployeePkid"]) : 0,
                    EmployeeCode = aDataReader["EmployeeCode"] != DBNull.Value ? aDataReader["EmployeeCode"].ToString() : "",
                    CurrentRank = aDataReader["CurrentRank"] != DBNull.Value ? aDataReader["CurrentRank"].ToString() : "",
                    SerialNumber = aDataReader["SerialNumber"] != DBNull.Value ? aDataReader["SerialNumber"].ToString() : "",
                    SerialNumber_Myan = aDataReader["SerialNumber_Myan"] != DBNull.Value ? aDataReader["SerialNumber_Myan"].ToString() : "",
                    Name = aDataReader["Name"] != DBNull.Value ? aDataReader["Name"].ToString() : "",
                    FatherName = aDataReader["Father_Name"] != DBNull.Value ? aDataReader["Father_Name"].ToString() : "",
                    MotherName = aDataReader["Mother_Name"] != DBNull.Value ? aDataReader["Mother_Name"].ToString() : "",
                    Gender = aDataReader["Gender"] != DBNull.Value ? aDataReader["Gender"].ToString() : "",
                    Religion = aDataReader["Religion"] != DBNull.Value ? aDataReader["Religion"].ToString() : "",
                    Race = aDataReader["Race"] != DBNull.Value ? aDataReader["Race"].ToString() : "",
                    Nrcnumber = aDataReader["Nrcnumber"] != DBNull.Value ? aDataReader["Nrcnumber"].ToString() : "",
                    PlaceOfBirth = aDataReader["PlaceOfBirth"] != DBNull.Value ? aDataReader["PlaceOfBirth"].ToString() : "",
                    EducationType = aDataReader["EducationType"] != DBNull.Value ? aDataReader["EducationType"].ToString() : "",
                    EducationTypeCode = aDataReader["EducationTypeCode"] != DBNull.Value ? aDataReader["EducationTypeCode"].ToString() : "",
                    PlaceOfBirthName = aDataReader["PlaceOfBirthName"] != DBNull.Value ? aDataReader["PlaceOfBirthName"].ToString() : "",
                    Occupation = aDataReader["Occupation"] != DBNull.Value ? aDataReader["Occupation"].ToString() : "",
                    DateOfBirthString = aDataReader["DateOfBirth"] != DBNull.Value ? aDataReader["DateOfBirth"].ToString() : "",
                    EyeColor = aDataReader["EyeColor"] != DBNull.Value ? aDataReader["EyeColor"].ToString() : "",
                    Height = aDataReader["Height"] != DBNull.Value ? aDataReader["Height"].ToString() : "",
                    Mark = aDataReader["Mark"] != DBNull.Value ? aDataReader["Mark"].ToString() : "",
                    OtherName = aDataReader["OtherName"] != DBNull.Value ? aDataReader["OtherName"].ToString() : "",
                    JoinDateString = aDataReader["JoinDate"] != DBNull.Value ? aDataReader["JoinDate"].ToString() : "",
                    CurrentRankDate = aDataReader["CurrentRankDate"] != DBNull.Value ? aDataReader["CurrentRankDate"].ToString() : "",
                    TownshipCode = aDataReader["TownshipCode"] != DBNull.Value ? aDataReader["TownshipCode"].ToString() : "",
                    Township = aDataReader["Township"] != DBNull.Value ? aDataReader["Township"].ToString() : "",
                    Age = aDataReader["Age"] != DBNull.Value ? Convert.ToInt32(aDataReader["Age"]) : 0,
                    Address = aDataReader["Address"] != DBNull.Value ? aDataReader["Address"].ToString() : "",
                    DearestPerson = aDataReader["DearestPerson"] != DBNull.Value ? aDataReader["DearestPerson"].ToString() : "",
                    Ancestor = aDataReader["Ancestor"] != DBNull.Value ? aDataReader["Ancestor"].ToString() : "",
                    IsActive = aDataReader["IsActive"] != DBNull.Value ? aDataReader["IsActive"].ToString() : "",
                    IsDeleted = aDataReader["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(aDataReader["IsDeleted"]) : false,
                    IsRecordEdited = aDataReader["IsRecordEdited"] != DBNull.Value ? Convert.ToBoolean(aDataReader["IsRecordEdited"]) : false,
                    CreatedDate = aDataReader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(aDataReader["CreatedDate"]) : DateTime.Now,
                    CreatedBy = aDataReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(aDataReader["CreatedBy"]) : 0
                };
            }
        }
    }
}
