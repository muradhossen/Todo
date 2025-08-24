using Application.RepositoryInterfaces.Base;
using Domain.Entities.Users;

namespace Application.RepositoryInterfaces.Users;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsExistAsync(string email);
}
