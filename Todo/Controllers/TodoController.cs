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

    }
}
