using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class TDTask
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? DueDate { get; set; }

    public int Priority { get; set; }

    public bool IsCompleted { get; set; }

    public bool RowStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual UsUser CreatedByNavigation { get; set; }

    public virtual ICollection<TDTaskCategory> TDTaskCategories { get; set; } = new List<TDTaskCategory>();

    public virtual UsUser UpdatedByNavigation { get; set; }

    public virtual UsUser User { get; set; }
}
