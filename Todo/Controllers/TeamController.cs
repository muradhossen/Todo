using Application.Common;
using Application.DTOs.Teams;
using Application.DTOs.Users;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Controllers.Base;

namespace Todo.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class TeamController : BaseApiController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            this._teamService = teamService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPaginatedTeams([FromQuery] TeamPageParams pageParam)
        {
            var result = await _teamService.GetPagedUsersAsync(pageParam);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teamService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(Result.Failure("User not found!"));
            }

            return Ok(Result<TeamDTO>.Success((new TeamDTO(result.Id, result.Name, result.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamCreateDTO request)
        {

            var result = await _teamService.CreateTeamAsync(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeamUpdateDTO request)
        {

            var result = await _teamService.UpdateTeamAsync(id, request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var team = await _teamService.GetByIdAsync(id);

            if (team == null) return NotFound(Result.Failure("Team not found!"));

            var isDeleted = await _teamService.RemoveAsync(team);

            if (isDeleted)
            {
                return Ok(Result.Success());
            }

            return BadRequest(Result.Failure("Failed to delete Team!"));
        }

    }
}
