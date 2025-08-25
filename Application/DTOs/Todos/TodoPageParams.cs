using Application.Common.Pagination;
using Domain.Enums;

namespace Application.DTOs.Todos;

public class TodoPageParams : PageParam
{
    public StatusEnum? Status { get; set; }
    public int? AssignTo { get; set; }
    public DateTime? DueDate { get; set; }
}
