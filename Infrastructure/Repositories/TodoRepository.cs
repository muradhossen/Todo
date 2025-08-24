using Application.RepositoryInterfaces;
using Domain.Entities.Todos;
using Infrastructure.Persistance;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore; 

namespace Infrastructure.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync()
        {
            return await _dbContext.Todos.AsNoTracking().ToListAsync();
        }
    }
}
