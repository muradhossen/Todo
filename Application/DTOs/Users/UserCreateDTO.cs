namespace Application.DTOs.Users;

public record UserCreateDTO(string FullName, string Email, int Role,int? TeamId, string Password);
