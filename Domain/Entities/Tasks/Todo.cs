using Domain.Common;
using Domain.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Tasks;

public class Todo : BaseEntity<long>
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public int Status { get; set; }
    public User AssignToUser { get; set; }
    public int? AssignToUserId { get; set; }
    public User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public DateTime DueDate { get; set; }

}
