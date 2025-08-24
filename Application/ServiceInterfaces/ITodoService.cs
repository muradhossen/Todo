using Application.DTOs.Todos;
using Application.ServiceInterfaces.Base;
using Domain.Entities.Todos;

namespace Application.ServiceInterfaces;

public interface ITodoService : IService<Todo>
{
    Task<IEnumerable<TodoDTO>> GetTodosAsync();
}
