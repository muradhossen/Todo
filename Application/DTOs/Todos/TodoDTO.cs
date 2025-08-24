namespace Application.DTOs.Todos;

public record TodoDTO(long Id, string Title, string Description, DateTime? StartDate, DateTime? EndDate);
