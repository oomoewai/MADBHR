using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class EmployeeDAO : GenericCommonExtensions<TbEmployee>
    {

        public dynamic SaveEmployee(IDbCommand cmd,TbEmployee employee,int userId)
        {
            try
            {
                cmd.CommandText = "Sp_Employee_Save";
                cmd.Parameters.Clear();


                cmd.AddParameter("@EmployeePkid", 0);
                cmd.AddParameter("@EmployeeCode", employee.EmployeeCode);
                cmd.AddParameter("@SerialNumber", employee.SerialNumber);
                cmd.AddParameter("@Name", employee.Name);
                cmd.AddParameter("@FatherName", employee.FatherName);
                cmd.AddParameter("@MotherName", employee.MotherName);
                cmd.AddParameter("@Gender", employee.Gender);
                cmd.AddParameter("@Religion",employee.Religion);
                cmd.AddParameter("@Race", employee.Race);
                cmd.AddParameter("@NRCNumber", employee.Nrcnumber);
                cmd.AddParameter("@Education", employee.EducationTypeCode);
                cmd.AddParameter("@Occupation", employee.Occupation);
                cmd.AddParameter("@DateOfBirth", employee.DateOfBirth);
                cmd.AddParameter("@EyeColor", employee.EyeColor);
                cmd.AddParameter("@Height", employee.Height);
                cmd.AddParameter("@Mark", employee.Mark);
                cmd.AddParameter("@OtherName", employee.OtherName);
                cmd.AddParameter("@JoinDate", employee.JoinDate);
                cmd.AddParameter("@TownshipCode", employee.TownshipCode);
                cmd.AddParameter("@Address", employee.Address);
                cmd.AddParameter("@DearestPerson", employee.DearestPerson);
                cmd.AddParameter("@Ancestor", employee.Ancestor);
                cmd.AddParameter("@IsActive", "Active");
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@IsRecordEdited",false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", null);
                cmd.AddParameter("@ProfilePic", employee.ProfilePic);
                cmd.AddParameter("@NrcPic", employee.Nrcpic);
                cmd.AddParameter("@Form66Pic", employee.Form66Pic);


                return employee;
            }
            catch(Exception ex)
            {

            }
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbEmployee aInfo, string prefix)
        { }
    }
}
