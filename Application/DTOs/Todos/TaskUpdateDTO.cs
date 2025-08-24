namespace Application.DTOs.Todos;

public record TaskUpdateDTO(string Title, string Description, DateTime? StartDate, DateTime? EndDate);

