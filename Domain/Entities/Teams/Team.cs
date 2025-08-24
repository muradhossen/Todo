using Domain.Common;
using Domain.Entities.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Teams;

public class Team : BaseEntity<int>
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Tasks.Task> Tasks { get; set; } = new List<Tasks.Task>();
}
