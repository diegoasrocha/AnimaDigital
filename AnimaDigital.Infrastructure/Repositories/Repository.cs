using AnimaDigital.Infrastructure.DBConfiguration;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq; 

namespace AnimaDigital.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AnimaContext dbContext;

        protected readonly DbSet<TEntity> dbSet;

        protected Repository(AnimaContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            entity = dbSet.Add(entity).Entity;
            this.Save();

            return entity;
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            return this.Save();
        }

        public IEnumerable<TEntity> GetAll() => dbSet.AsEnumerable(); 

        public TEntity GetById(object id) => dbSet.Find(id);

        public bool Remove(object id)
        {
            TEntity entity = this.GetById(id);

            if (entity == null) return false;

            return this.Remove(entity) > 0;

        }

        public int Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            return this.Save();
        }

        public int RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            return this.Save();
        }

        public int Update(TEntity entity)
        {
            dbSet.Update(entity);
            return this.Save();
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            return this.Save();
        }

        public int Save() => dbContext.SaveChanges(); 
    }
}
