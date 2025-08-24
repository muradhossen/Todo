using Domain.Common;
using Domain.Entities.Tasks;
using Domain.Entities.Teams;
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
    public ICollection<Task> Tasks { get; set; } = new List<Task>();

    public Team Team { get; set; }
    public int? TeamId { get; set; }
}
