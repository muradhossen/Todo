using Application.RepositoryInterfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Base;


public class Repository<T> : IRepository<T> where T : class
{
    DbContext _dbContext;

    DbSet<T> Table
    {
        get
        {
            return _dbContext.Set<T>();
        }
    }

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual bool Add(T entity)
    {

        Table.Add(entity);
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> AddAsync(T entity)
    {

        await Table.AddAsync(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public virtual bool Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return _dbContext.SaveChanges() > 0;
    }

    public virtual bool Remove(T entity)
    {
        Table.Remove(entity);
        return _dbContext.SaveChanges() > 0;
    }

    public virtual async Task<bool> RemoveAsync(T entity)
    {
        Table.Remove(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveRangeAsync(ICollection<T> entity)
    {
        Table.RemoveRange(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public virtual T GetById(object id)
    {
        return Table.Find(id);
    }

    public virtual async Task<T> GetByIdAsync(object id)
    {
        return await Table.FindAsync(id);
    }

    public virtual ICollection<T> GetAll()
    {
        return Table.ToList();
    }

    public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate)
    {
        return Table.Where(predicate).ToList();
    }

    public virtual bool UpdateRange(ICollection<T> entity)
    {
        Table.UpdateRange(entity);

        return _dbContext.SaveChanges() > 0;
    }

    public virtual bool RemoveRange(ICollection<T> entity)
    {
        Table.RemoveRange(entity);
        return _dbContext.SaveChanges() > 0;
    }


    public virtual async Task<bool> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> UpdateRangeAsync(ICollection<T> entity)
    {
        Table.UpdateRange(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<ICollection<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public virtual async Task<ICollection<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.Where(predicate).ToListAsync();
    }

    public virtual bool AddRange(ICollection<T> entities)
    {
        _dbContext.Set<T>().AddRange(entities);
        return _dbContext.SaveChanges() > 0;
    }

    public virtual async Task<bool> AddRangeAsync(ICollection<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null)
    {
        if (predicate == null)
        {
            return Table.AsQueryable();
        }

        return Table.Where(predicate).AsQueryable();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return Table.FirstOrDefault(predicate);
    }
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.FirstOrDefaultAsync(predicate);
    }
    public virtual async Task<int> CountAsync()
    {
        return await Table.CountAsync();

    }
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> where)
    {
        return await Table.CountAsync(where);

    }


    /// <summary>
    /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
    /// </summary>
    public virtual IQueryable<T> TableNoTracking => Table.AsNoTracking();

}
