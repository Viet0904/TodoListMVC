using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class UsGroup
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Note { get; set; }

    public bool RowStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual ICollection<UsGroupPermission> UsGroupPermissions { get; set; } = new List<UsGroupPermission>();

    public virtual ICollection<UsUser> UsUsers { get; set; } = new List<UsUser>();
}
