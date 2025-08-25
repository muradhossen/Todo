using Application.Common;
using Application.DTOs.Todos;
using Application.DTOs.Users;
using Application.Extensions;
using Application.ServiceInterfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Controllers.Base;

namespace Todo.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class TodosController(ITodoService todoService) : BaseApiController
    {
        private readonly ITodoService _todoService = todoService;

        [HttpGet]
        public async Task<IActionResult> GetPaginatedTasks([FromQuery] TodoPageParams pageParam)
        {
            var result = await _todoService.GetPagedTodosAsync(pageParam);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _todoService.GetTodoByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result); 
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateDTO request)
        {

            var result = await _todoService.CreateTodoAsync(createdByUserId: User.GetUserId(), request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TodoUpdateDTO request)
        {

            var result = await _todoService.UpdateTodoAsync(id, request);


            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var isDeleted = await _todoService.RemoveAsync(await _todoService.GetByIdAsync(id));

            if (isDeleted)
            {
                return Ok(Result.Success());
            }

            return BadRequest(Result.Failure("Failed to delete todo item!"));
        }

    }
}
