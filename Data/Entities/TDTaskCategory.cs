using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class TDTaskCategory
{
    public string TaskId { get; set; }

    public string CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual TDCategory Category { get; set; }

    public virtual TDTask Task { get; set; }
}
