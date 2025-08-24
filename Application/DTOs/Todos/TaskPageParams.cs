using Application.Common.Pagination;
using Domain.Enums;

namespace Application.DTOs.Todos;

public class TaskPageParams : PageParam
{
    public StatusEnum Status { get; set; }
    public int AssignTo { get; set; }
}
