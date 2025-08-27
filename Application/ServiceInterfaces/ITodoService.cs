using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Todos;
using Application.ServiceInterfaces.Base;


namespace Application.ServiceInterfaces;

public interface ITodoService : IService<Domain.Entities.Tasks.Todo>
{ 
    Task<Result<PagedList<TodoDTO>>> GetPagedTodosAsync(TodoPageParams pageParam);
    Task<Result<TodoDTO>> CreateTodoAsync(int createdByUserId, TodoCreateDTO request);
    Task<Result<TodoDTO>> UpdateTodoAsync(long id, TodoUpdateDTO request);
    Task<Result<TodoDTO>> GetTodoByIdAsync(long id);
}
