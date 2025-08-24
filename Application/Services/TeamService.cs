using Application.Common.Pagination;
using Application.Common;
using Application.DTOs.Users;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Application.Services.Base;
using Domain.Entities.Teams;
using Domain.Entities.Users;
using Domain.Enums;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs.Teams;
using System.Net.Http.Headers;

namespace Application.Services
{
    public class TeamService : Service<Team>, ITeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository) : base(repository)
        {
            this._repository = repository;
        }


        public async Task<PagedList<TeamDTO>> GetPagedUsersAsync(TeamPageParams pageParam)
        {
            var query = _repository.TableNoTracking.AsQueryable();


            if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
            {
                string searchKey = pageParam.SearchKey.ToLower().Trim();

                query = query
                    .Where(c => c.Name.ToLower().Contains(searchKey));
            }

            query = query.OrderByDescending(c => c.Id);

            if (pageParam.Order == -1)
            {
                query = query.OrderByDescending(c => c.Id);
            }
            else
            {
                query = query.OrderBy(c => c.Id);
            }
            var mappedDate = query.Select(x => new TeamDTO(x.Id, x.Name, x.Description));

            return await PagedList<TeamDTO>.CreateAsync(mappedDate, pageParam.PageSize, pageParam.PageNumber);
        }



        public async Task<Result> CreateTeamAsync(TeamCreateDTO request)
        {
            var team = new Team()
            {
                Name = request.Name,
                Description = request.Description,
            };
            if (await _repository.AddAsync(team))
            {
                var response = new TeamDTO(team.Id, team.Name, team.Description);

                return Result<TeamDTO>.Success(response);
            }

            return Result.Failure("Failed to add new team!");
        }

        public async Task<Result> UpdateTeamAsync(int id, TeamUpdateDTO request)
        {
            var team = await _repository.GetByIdAsync(id);

            if (team == null)
            {
                return Result<UserDto>.Failure("Team dose not exist!");
            }


            team.Name = request.Name;
            team.Description = request.Description;
            team.Id = id;

            if (await _repository.UpdateAsync(team))
            {
                var response = new TeamDTO(team.Id, team.Name, team.Description);

                return Result<TeamDTO>.Success(response);
            }

            return Result.Failure($"Failed to update {team.Name} team!");
        }
    }
}
