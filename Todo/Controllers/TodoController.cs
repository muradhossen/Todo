using Application.DTOs.Todos;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Controllers.Base;

namespace Todo.Controllers
{
    public class TodoController(ITodoService todoService) : BaseApiController
    {
        private readonly ITodoService _todoService = todoService;

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var result = await _todoService.GetTodosAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _todoService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateDTO request)
        {
            var result = await _todoService.AddAsync(new Domain.Entities.Todos.Todo
            {
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedOn = System.DateTime.Now,

            });

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Create(long id, [FromBody] TodoUpdateDTO request)
        {
            var result = await _todoService.UpdateAsync(new Domain.Entities.Todos.Todo
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedOn = System.DateTime.Now,

            });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var isDeleted = await _todoService.RemoveAsync(_todoService.GetById(id));

            if (isDeleted)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
