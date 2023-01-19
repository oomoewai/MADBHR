using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IRelationshipServices
    {
        Task<dynamic> SaveRelationShip(TbRelationship relationship, int userId, int Id);
        void DeleteRelationship(int RelationshipPkid, int userId);

    }
}
