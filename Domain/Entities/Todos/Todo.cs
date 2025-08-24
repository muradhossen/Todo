using Domain.Common;
using System;

namespace Domain.Entities.Todos;

public class Todo : BaseEntity<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }

}
