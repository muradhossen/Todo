using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Todos;
using Application.RepositoryInterfaces;
using Application.RepositoryInterfaces.Users;
using Application.ServiceInterfaces;
using Application.Services.Base;
using Domain.Entities.Tasks;
using Domain.Enums;
using System.Threading.Tasks;

namespace Application.Services;

public class TodoService : Service<Todo>, ITodoService
{
    private readonly ITaskRepository _repository;
    private readonly IUserRepository _userRepository;

    public TodoService(ITaskRepository repository
        , IUserRepository userRepository) : base(repository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<TodoDTO>> GetTasksAsync()
    {
        var todos = await _repository.GetTodosAsync();

        return todos.Select(t => new TodoDTO(t.Id, t.Title, t.Description, t.DueDate));
    }

    public async Task<Result<PagedList<TodoDTO>>> GetPagedTodosAsync(TodoPageParams pageParam)
    {
        var query = _repository.TableNoTracking.AsQueryable();


        if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
        {
            string searchKey = pageParam.SearchKey.ToLower().Trim();

            query = query
                .Where(c => c.Title.ToLower().Contains(searchKey));


            if (pageParam.AssignTo != null)
            {
                query = query.Where(c => c.AssignToUserId == pageParam.AssignTo);
            }

            if (pageParam.Status != null)
            {
                query = query.Where(c => c.Status == (int)pageParam.Status);
            }

            if (pageParam.DueDate != null)
            {
                query = query.Where(c => c.DueDate == pageParam.DueDate);
            }
        }

        query = query.OrderByDescending(c => c.Id);

        if (pageParam.Order == -1)
        {
            query = query.OrderByDescending(c => c.Id);
        }

        var mappedDate = query.Select(x => new TodoDTO(x.Id, x.Title, x.Description, x.DueDate));

        var pagedDate = await PagedList<TodoDTO>.CreateAsync(mappedDate, pageParam.PageSize, pageParam.PageNumber);


        return Result<PagedList<TodoDTO>>.Success(pagedDate);
    }

    public async Task<Result<TodoDTO>> CreateTodoAsync(int createdByUserId, TodoCreateDTO request)
    {
        var isUserExist = await _userRepository.IsExistAsync(request.AssignToUserId);

        if (!isUserExist)
        {
            return Result<TodoDTO>.Failure("Assigned user dosen't exist!");
        }

        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            AssignToUserId = request.AssignToUserId,
            Status = (int)StatusEnum.Todo,
            CreatedByUserId = createdByUserId
        };

        var isCreated = await _repository.AddAsync(todo);


        if (isCreated)
        {
            return Result<TodoDTO>.Success(new TodoDTO(todo.Id, todo.Title, todo.Description, todo.DueDate));
        }

        return Result<TodoDTO>.Failure($"Failed to update todo item!");
    }

    public async Task<Result<TodoDTO>> UpdateTodoAsync(long id, TodoUpdateDTO request)
    {
        var todo = await _repository.GetByIdAsync(id);


        if (todo is null)
        {
            return Result<TodoDTO>.Failure($" {request.Title} todo item dosen't exist!");
        }

        todo.Description = request.Description;
        todo.Title = request.Title;
        todo.DueDate = request.DueDate;


        var isUpdated = await _repository.UpdateAsync(todo);

        if (isUpdated)
        {
            return Result<TodoDTO>.Success(new TodoDTO(todo.Id, todo.Title, todo.Description, todo.DueDate));
        }

        return Result<TodoDTO>.Failure("Failed to update todo");
    }

    public async Task<Result<TodoDTO>> GetTodoByIdAsync(long id)
    {
        var todo = await _repository.GetByIdAsync(id);

        if (todo is null)
        {
            return Result<TodoDTO>.Failure("Todo item dosen't exist!");
        }

        return Result<TodoDTO>.Success(new TodoDTO(todo.Id,todo.Title,todo.Description,todo.DueDate));
    }
}
