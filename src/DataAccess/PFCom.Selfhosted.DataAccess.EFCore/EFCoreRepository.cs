using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PFCom.Selfhosted.DataAccess.EFCore
{
    public class EfCoreRepository<T> : IBaseRepository<T> where T : class
    {
        private DbSet<T> DbSet { get; }

        public EfCoreRepository(DataContext context)
        {
            this.DbSet = context.Set<T>();
        }
        
        public IQueryable<T> GetAll()
        {
            return this.DbSet;
        }

        public T Get(params object[] primaryKey)
        {
            return this.DbSet.Find(primaryKey);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            
            this.DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbSet.Remove(entity);
        }

        public void DeleteAll()
        {
            this.DbSet.RemoveRange(this.DbSet);
        }
    }
}
