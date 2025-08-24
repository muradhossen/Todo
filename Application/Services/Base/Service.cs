using Application.RepositoryInterfaces.Base;
using Application.ServiceInterfaces.Base;
using System.Linq.Expressions;

namespace Application.Services.Base;

public class Service<T> : IService<T> where T : class
{
    IRepository<T> _repository;

    public Service(IRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual bool Add(T entity)
    {
        return _repository.Add(entity);
    }

    public virtual bool Update(T entity)
    {
        return _repository.Update(entity);
    }

    public virtual bool Remove(T entity)
    {
        return _repository.Remove(entity);
    }
    public virtual async Task<bool> RemoveAsync(T entity)
    {
        return await _repository.RemoveAsync(entity);
    }

    public virtual T GetById(object id)
    {
        return _repository.GetById(id);
    }

    public virtual ICollection<T> GetAll()
    {
        return _repository.GetAll();
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        return await _repository.AddAsync(entity);
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        return await _repository.UpdateAsync(entity);
    }

    public virtual async Task<ICollection<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public virtual bool AddRange(ICollection<T> entity)
    {
        return _repository.AddRange(entity);
    }

    public async Task<bool> AddRangeAsync(ICollection<T> entities)
    {
        return await _repository.AddRangeAsync(entities);
    }

    public bool UpdateRange(ICollection<T> entity)
    {
        return _repository.UpdateRange(entity);
    }

    public async Task<bool> UpdateRangeAsync(ICollection<T> entity)
    {
        return await _repository.UpdateRangeAsync(entity);
    }

    public bool RemoveRange(ICollection<T> entity)
    {
        return _repository.RemoveRange(entity);
    }

    public async Task<bool> RemoveRangeAsync(ICollection<T> entity)
    {
        return await _repository.RemoveRangeAsync(entity);
    }

    public virtual async Task<T> GetByIdAsync(object id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public ICollection<T> Get(Expression<Func<T, bool>> predicate)
    {
        return _repository.Get(predicate);
    }

    public async Task<ICollection<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _repository.GetManyAsync(predicate);
    }

}
