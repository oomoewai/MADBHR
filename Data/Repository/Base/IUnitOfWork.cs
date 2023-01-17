using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MADBHR_Data.Repository.Base
{
   public interface IUnitOfWork:IDisposable
    {
        #region [Repository Declaration]
   
        IGenericRepository<TbUser> TbUserRepository { get; }
        IGenericRepository<TbEmployee> TbEmployeeRepository { get; }
        #endregion

        void Commit();
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

    }
}
