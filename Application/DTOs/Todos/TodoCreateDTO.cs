namespace Application.DTOs.Todos;

public record TodoCreateDTO(string Title, string Description, int AssignToUserId, DateTime DueDate);
