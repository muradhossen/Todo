using System.Linq.Expressions;

namespace Application.RepositoryInterfaces.Base;


public interface IRepository<T> where T : class
{
    bool Add(T entity);
    Task<bool> AddAsync(T entity);

    bool AddRange(ICollection<T> entity);
    Task<bool> AddRangeAsync(ICollection<T> entities);

    bool Update(T entity);

    Task<bool> UpdateAsync(T entity);

    bool UpdateRange(ICollection<T> entity);

    Task<bool> UpdateRangeAsync(ICollection<T> entity);
    bool Remove(T entity);
    Task<bool> RemoveAsync(T entity);
    bool RemoveRange(ICollection<T> entity);
    Task<bool> RemoveRangeAsync(ICollection<T> entity);
    T GetById(object id);
    Task<T> GetByIdAsync(object id);
    ICollection<T> GetAll();

    Task<ICollection<T>> GetAllAsync();

    ICollection<T> Get(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null);

    T GetFirstOrDefault(Expression<Func<T, bool>> predicate);

    Task<ICollection<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>> where);
    Task<int> CountAsync();
    #region Properties

    /// <summary>
    /// Gets a table
    /// </summary>
    //IQueryable<T> Table { get; }

    /// <summary>
    /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
    /// </summary>
    IQueryable<T> TableNoTracking { get; }

    #endregion
}