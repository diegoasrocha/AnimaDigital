using System.Collections.Generic;

namespace AnimaDigital.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        int AddRange(IEnumerable<TEntity> entities);


        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();

        int Update(TEntity entity);
        int UpdateRange(IEnumerable<TEntity> entities);

        bool Remove(object id);
        int Remove(TEntity entity);
        int RemoveRange(IEnumerable<TEntity> entities);

        int Save();
    }
}
