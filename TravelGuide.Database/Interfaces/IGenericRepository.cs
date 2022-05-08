using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TravelGuide.Database.Interfaces
{
    public interface IGenericRepository<TEntity> 
    {
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task DeleteByCondition(Expression<Func<TEntity, bool>> where = null);
        Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> where = null);
        Task<IEnumerable<TEntity>> SelectAllAsync(Expression<Func<TEntity, bool>> where = null);

    }
}
