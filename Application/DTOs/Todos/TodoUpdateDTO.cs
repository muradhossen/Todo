namespace Application.DTOs.Todos;

public record TodoUpdateDTO(string Title, string Description, DateTime? StartDate, DateTime? EndDate);

