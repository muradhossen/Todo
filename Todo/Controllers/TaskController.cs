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

    public class TaskController(ITaskService todoService) : BaseApiController
    {
        private readonly ITaskService _taskService = todoService;

        [Authorize(Policy = "EmployeePolicy")]
        [HttpGet]

        [HttpGet]
        public async Task<IActionResult> GetPaginatedTasks([FromQuery] TaskPageParams pageParam)
        {
            var result = await _taskService.GetPagedTasksAsync(pageParam);

            return Ok(result);
        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _taskService.GetByIdAsync(id);

            return Ok(result);
        }

        [Authorize(Policy = "ManagerPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDTO request)
        {

            var userId = User.GetUserId();

            var result = await _taskService.AddAsync(new Domain.Entities.Tasks.Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                CreatedByUserId = userId,
                AssignToUserId = request.AssignToUserId,
                Status = (int)StatusEnum.Todo
            });

            return Ok(result);
        }

        [Authorize(Policy = "ManagerPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TaskUpdateDTO request)
        {

            var task = await _taskService.GetByIdAsync(id);


            if (task is null)
            {
                return NotFound(Result.Failure("Task dosen't exist!"));
            }

            task.Description = request.Description;
            task.Title = request.Title;

            var result = await _taskService.UpdateAsync(task);

            return Ok(result);
        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(long id, [FromBody] TaskUpdateStatusDTO request)
        {
            var userId = User.GetUserId();

            var result = await _taskService.UpdateStatusAsync(userId, id, request);


            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var isDeleted = await _taskService.RemoveAsync(_taskService.GetById(id));

            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
