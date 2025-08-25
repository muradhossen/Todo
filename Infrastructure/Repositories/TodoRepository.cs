using Application.RepositoryInterfaces;
using Domain.Entities.Tasks;
using Infrastructure.Persistance;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore; 

namespace Infrastructure.Repositories
{
    public class TodoRepository : Repository<Domain.Entities.Tasks.Todo>, ITaskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Entities.Tasks.Todo>> GetTodosAsync()
        {
            return await _dbContext.Todos.AsNoTracking().ToListAsync();
        }
    }
}
