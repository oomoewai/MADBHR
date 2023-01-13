using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Data.Repository.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void InsertList(IEnumerable<TEntity> lstentity);

        void InsertListAsync(IEnumerable<TEntity> lstentity);

        void Update(TEntity entityToUpdate);

        void UpdateList(IEnumerable<TEntity> entityToUpdateLst);

        void Delete(object id);
        void Delete(TEntity entity);

        void DeleteList(IEnumerable<TEntity> entityToDeleteLst);

        void CopyPropertiesOfDifferentType(object Source, object Target, string[] SkipPropertyNames = null, bool SkipDynamicProxyRelated = true);

        void DisposeTransaction();
    }
}
