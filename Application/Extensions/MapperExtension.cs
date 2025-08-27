using Application.DTOs.Todos;
using Domain.Entities.Tasks;
using Domain.Enums;

namespace Application.Extensions;

public static class MapperExtension
{
    public static Todo ToEntity(this TodoCreateDTO dto)
    {
        return new Todo
        {
            Title = dto.Title,
            Status = (int)StatusEnum.Todo,
            AssignToUserId = dto.AssignToUserId,
            CreatedByUserId = dto.CreatedByUserId,
            Description = dto.Description,
            DueDate = dto.DueDate
        };
    }

    public static Todo ToEntity(this TodoUpdateDTO dto)
    {
        return new Todo
        {
            Title = dto.Title,
            Status = (int)dto.Status,
            AssignToUserId = dto.AssignToUserId,
            CreatedByUserId = dto.CreatedByUserId,
            Description = dto.Description,
            DueDate = dto.DueDate
        };
    }

    public static TodoDTO ToDto(this Todo entity)
    {
        return new TodoDTO(entity.Id, entity.Title, entity.Description,entity.AssignToUserId,entity.CreatedByUserId ,entity.DueDate);
    }
}
