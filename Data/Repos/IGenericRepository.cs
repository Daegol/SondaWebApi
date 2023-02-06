using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includes);
        Task<T> GetById(int id);
        Task<T> GetByIdWithInclude(int id, params Expression<Func<T, object>>[] includes);
        Task<T> Find(Expression<Func<T, bool>> match);
        List<T> FindAll(Expression<Func<T, bool>> match);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> Insert(T entity);
        Task<bool> BulkInsert(List<T> entities);
        Task<T> Update(T entity);
        Task<int> Delete(T entity);
    }
}
