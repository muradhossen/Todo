using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Teams;
using Application.DTOs.Users;
using Application.ServiceInterfaces.Base;
using Domain.Entities.Teams;

namespace Application.ServiceInterfaces
{
    public interface ITeamService : IService<Team>
    {
        Task<PagedList<TeamDTO>> GetPagedUsersAsync(TeamPageParams pageParam);

        Task<Result> CreateTeamAsync(TeamCreateDTO request);
        Task<Result> UpdateTeamAsync(int id, TeamUpdateDTO request);

    }
}
