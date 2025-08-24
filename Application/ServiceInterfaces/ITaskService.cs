using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Todos;
using Application.ServiceInterfaces.Base;


namespace Application.ServiceInterfaces;

public interface ITaskService : IService<Domain.Entities.Tasks.Task>
{
    Task<IEnumerable<TaskDTO>> GetTasksAsync();
    Task<Result> UpdateStatusAsync(int userId, long id, TaskUpdateStatusDTO request);
    Task<PagedList<TaskDTO>> GetPagedTasksAsync(TaskPageParams pageParam);
}
