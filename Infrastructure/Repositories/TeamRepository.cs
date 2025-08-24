using Application.RepositoryInterfaces;
using Domain.Entities.Teams;
using Infrastructure.Persistance;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
