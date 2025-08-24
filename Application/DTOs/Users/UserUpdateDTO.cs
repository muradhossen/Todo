namespace Application.DTOs.Users;

public record UserUpdateDTO(string FullName, int Role, int? TeamId);
