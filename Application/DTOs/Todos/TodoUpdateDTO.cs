using Domain.Enums;

namespace Application.DTOs.Todos;

public record TodoUpdateDTO(string Title, string Description, StatusEnum Status,int AssignToUserId, DateTime DueDate);

