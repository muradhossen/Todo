using Application.DTOs;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Application.Services.Base;
using Domain.Entities.Todos;

namespace Application.Services;

public class TodoService : Service<Todo>, ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TodoDTO>> GetTodosAsync()
    {
        var todos = await _repository.GetTodosAsync();

        return todos.Select(t => new TodoDTO(t.Id, t.Title, t.Description, t.StartDate, t.EndDate));
    }
}
