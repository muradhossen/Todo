namespace Application.DTOs.Todos;

public record TodoCreateDTO(string Title, string Description, int AssignToUserId, int CreatedByUserId, DateTime DueDate);
