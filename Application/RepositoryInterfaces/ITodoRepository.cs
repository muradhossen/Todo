using Application.RepositoryInterfaces.Base;
using Domain.Entities.Todos;

namespace Application.RepositoryInterfaces;

public interface ITodoRepository : IRepository<Todo>
{
    Task<IEnumerable<Todo>> GetTodosAsync();
    //Task<Todo> GetByIdAsync(int id);
    //Task<Todo> CreateTodo(Todo todo);

    //Task<Todo> UpdateTodo(Todo todo);
    //Task<Todo> DeleteTodo(int id);
}
