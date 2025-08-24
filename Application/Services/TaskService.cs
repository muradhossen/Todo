using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Todos;
using Application.DTOs.Users;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Application.Services.Base;

namespace Application.Services;

public class TaskService : Service<Domain.Entities.Tasks.Task>, ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
    {
        var todos = await _repository.GetTodosAsync();

        return todos.Select(t => new TaskDTO(t.Id, t.Title, t.Description, t.DueDate));
    }

    public async Task<PagedList<TaskDTO>> GetPagedUsersAsync(TaskPageParams pageParam)
    {
        var query = _repository.TableNoTracking.AsQueryable();


        if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
        {
            string searchKey = pageParam.SearchKey.ToLower().Trim();

            query = query
                .Where(c => c.Title.ToLower().Contains(searchKey));
        }

        query = query.OrderByDescending(c => c.Id);

        if (pageParam.Order == -1)
        {
            query = query.OrderByDescending(c => c.Id);
        }

        var mappedDate = query.Select(x => new TaskDTO(x.Id,x.Title,x.Description,x.DueDate));

        return await PagedList<TaskDTO>.CreateAsync(mappedDate, pageParam.PageSize, pageParam.PageNumber);
    }

    public async Task<Result> UpdateStatusAsync(int userId,long id, TaskUpdateStatusDTO request)
    {
        var task = await _repository.GetFirstOrDefaultAsync(c => c.Id == id);

        if (task == null)
        {
            return Result.Failure("Task not found!");
        }

        if (task.AssignToUserId != userId)
        {
            return Result.Failure("This task is not associated with you. You can't update the task status!");
        }

        var isUpdated = await _repository.UpdateAsync(task);


        if (isUpdated)
        {
            return Result<Domain.Entities.Tasks.Task>.Success(task);
        }

        return Result.Failure($"Failed to update task status {task.Title}!");

    }
}
