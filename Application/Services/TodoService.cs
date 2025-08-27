using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Todos;
using Application.Errors;
using Application.Extensions;
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

        var mappedDate = query.Select(x => x.ToDto());

        var pagedDate = await PagedList<TodoDTO>.CreateAsync(mappedDate, pageParam.PageSize, pageParam.PageNumber);


        return Result<PagedList<TodoDTO>>.Success(pagedDate);
    }

    public async Task<Result<TodoDTO>> CreateTodoAsync(int createdByUserId, TodoCreateDTO request)
    {
        var isUserExist = await _userRepository.IsExistAsync(request.AssignToUserId);

        if (!isUserExist)
        {
            return Result<TodoDTO>.Failure(TodoError.AssignedUserDoseNotExist());
        } 

        var todo = request.ToEntity(createdByUserId);

        var isCreated = await _repository.AddAsync(todo); 

        if (isCreated)
        {
            return Result<TodoDTO>.Success(todo.ToDto());
        }

        return Result<TodoDTO>.Failure(TodoError.FailedToUpdate());
    }

    public async Task<Result<TodoDTO>> UpdateTodoAsync(long id, TodoUpdateDTO request)
    {
        var todo = await _repository.GetByIdAsync(id);


        if (todo is null)
        {
            return Result<TodoDTO>.Failure(TodoError.NotFound(todo.Title));
        } 

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.DueDate = request.DueDate;
        todo.Status = (int)request.Status;
        todo.AssignToUserId = request.AssignToUserId;

        var isUpdated = await _repository.UpdateAsync(todo);

        if (isUpdated)
        {
            return Result<TodoDTO>.Success(todo.ToDto());
        }

        return Result<TodoDTO>.Failure("Failed to update todo!");
    }

    public async Task<Result<TodoDTO>> GetTodoByIdAsync(long id)
    {
        var todo = await _repository.GetByIdAsync(id);

        if (todo is null)
        {
            return Result<TodoDTO>.Failure(TodoError.NotFound());
        }

        return Result<TodoDTO>.Success(todo.ToDto());
    }
}
