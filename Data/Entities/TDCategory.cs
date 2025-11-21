using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class TDCategory
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }

    public bool RowStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual ICollection<TDTaskCategory> TDTaskCategories { get; set; } = new List<TDTaskCategory>();

    public virtual UsUser User { get; set; }
}
