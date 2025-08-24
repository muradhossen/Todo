namespace Application.DTOs;

public record TodoDTO(long Id, string Title, string Description, DateTime? StartDate, DateTime? EndDate);
