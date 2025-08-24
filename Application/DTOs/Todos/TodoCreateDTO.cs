namespace Application.DTOs.Todos;

public record TodoCreateDTO(string Title, string Description, DateTime? StartDate, DateTime? EndDate);
