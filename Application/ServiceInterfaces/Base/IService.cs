using System.Linq.Expressions;

namespace Application.ServiceInterfaces.Base;
public interface IService<T> where T : class
{
    bool Add(T entity);
    Task<bool> AddAsync(T entity);
    bool Update(T entity);
    Task<bool> UpdateAsync(T entity);
    bool Remove(T entity);
    Task<bool> RemoveAsync(T entity);
    T GetById(object id);
    ICollection<T> GetAll();
    bool AddRange(ICollection<T> entity);
    Task<bool> AddRangeAsync(ICollection<T> entities);
    bool UpdateRange(ICollection<T> entity);
    Task<bool> UpdateRangeAsync(ICollection<T> entity);
    bool RemoveRange(ICollection<T> entity);
    Task<bool> RemoveRangeAsync(ICollection<T> entity);
    Task<T> GetByIdAsync(object id);
    ICollection<T> Get(Expression<Func<T, bool>> predicate);
    Task<ICollection<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
    Task<ICollection<T>> GetAllAsync();
}
