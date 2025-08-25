using Application.RepositoryInterfaces.Base;


namespace Application.RepositoryInterfaces;

public interface ITaskRepository : IRepository<Domain.Entities.Tasks.Todo>
{
    Task<IEnumerable<Domain.Entities.Tasks.Todo>> GetTodosAsync();

}
