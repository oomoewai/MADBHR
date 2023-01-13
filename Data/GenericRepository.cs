using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MADBHR_Data.Models;
using MADBHR_Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace MADBHR_Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        public readonly DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(MADBAdminSolutionContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void CopyPropertiesOfDifferentType(object Source, object Target, string[] SkipPropertyNames = null, bool SkipDynamicProxyRelated = true)
        {
            foreach (PropertyInfo propA in Source.GetType().GetProperties())
            {
                if (SkipDynamicProxyRelated && propA.GetType().ToString().Contains("Dynamic"))
                {
                    continue;
                }

                if (SkipPropertyNames != null && SkipPropertyNames.Any(d => d == propA.Name))
                {
                    continue;
                }

                PropertyInfo propB = Target.GetType().GetProperty(propA.Name);

                if (propB != null && propA.CanRead && propB.CanWrite)
                {

                    var sourceObjVal = propA.GetValue(Source, null);

                    //If optional parameter update null value

                    //if (sourceObjVal != null)
                    //{
                    propB.SetValue(Target, sourceObjVal, null);
                    //}
                }
            }
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteList(IEnumerable<TEntity> entityToDeleteLst)
        {
            dbSet.RemoveRange(entityToDeleteLst);
        }

        public void DisposeTransaction()
        {
            context.Database.CurrentTransaction.Dispose();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            //  When using Include, no proxies are required.
            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }

            }
            catch (Exception e)
            {
                return query.ToList();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            try
            {


                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }

            }
            catch (Exception e)
            {
                return await query.ToListAsync();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertList(IEnumerable<TEntity> lstentity)
        {
            dbSet.AddRange(lstentity);
        }

        public virtual async void InsertListAsync(IEnumerable<TEntity> lstentity)
        {
            await dbSet.AddRangeAsync(lstentity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateList(IEnumerable<TEntity> entityToUpdateLst)
        {
            dbSet.UpdateRange(entityToUpdateLst);
        }
    }
}
