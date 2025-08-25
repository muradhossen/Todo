using Domain.Common;
using Domain.Entities.Tasks; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Users;

public class User : BaseEntity<int>
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Email { get; set; }
    public int Role { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public ICollection<Todo> Tasks { get; set; } = new List<Todo>(); 
}
