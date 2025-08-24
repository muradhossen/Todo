using Application.RepositoryInterfaces.Base;


namespace Application.RepositoryInterfaces;

public interface ITaskRepository : IRepository<Domain.Entities.Tasks.Task>
{
    Task<IEnumerable<Domain.Entities.Tasks.Task>> GetTodosAsync();

}
