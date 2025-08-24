namespace Application.DTOs.Todos;

public record TaskCreateDTO(string Title, string Description, DateTime DueDate,int AssignToUserId);
