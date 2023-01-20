using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class RelationshipDAO:GenericCommonExtensions<TbRelationship>
    {
        public dynamic SaveRelationShip(IDbCommand cmd, TbRelationship relationship, int userId,int Id)
        {
            try
            {
                cmd.CommandText = "Sp_RelationShip_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", relationship.RelationshipPkid);
                cmd.AddParameter("@EmployeeCode", relationship.EmployeeCode);
                cmd.AddParameter("@RelationName", relationship.RelationName);
                cmd.AddParameter("@RelationDOB", relationship.RelationDob);
                cmd.AddParameter("@RelationOccupation", relationship.RelationOccupation);
                cmd.AddParameter("@RelationAddress", relationship.RelationAddress);
                cmd.AddParameter("@RelationFatherName", relationship.RelationFatherName);
                cmd.AddParameter("@RelationMotherName", relationship.RelationMotherName);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", relationship.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return relationship;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public void DeleteRelationship(IDbCommand cmd,int ReloationshipPkid,int userId)
        {

            cmd.CommandText = "Sp_Relationship_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@RelationshipPkid", ReloationshipPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbRelationship aInfo, string prefix)
        { }
    }
}
