namespace Application.DTOs.Todos;

public record TaskDTO(long Id, string Title, string Description, DateTime? DueDate);
