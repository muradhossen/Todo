using Application.RepositoryInterfaces.Users;
using Domain.Entities.Users;
using Infrastructure.Persistance;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

internal class UserRepository : Repository<User>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        this._dbContext = dbContext;
    }

    public Task<bool> IsExistAsync(string email)
    {
        return _dbContext.Users.AnyAsync(u => u.Email == email);
    }
}
