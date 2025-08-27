namespace Application.Errors;

public class TodoError
{
    public static string NotFound(string title) => $"{title} todo item dosen't exist!";
    public static string NotFound() => $"Todo item dosen't exist!";
    public static string FailedToUpdate() => "Failed to update!";
    public static string InternalError() => "Something wants wrong.Please try again later!";
    public static string AssignedUserDoseNotExist() => "Assigned user dosen't exist!!";
}
