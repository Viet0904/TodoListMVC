using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class SysMenu
{
    public string Name { get; set; }

    public string ParentMenu { get; set; }

    public string Note { get; set; }

    public int IsActive { get; set; }

    public virtual ICollection<UsGroupPermission> UsGroupPermissions { get; set; } = new List<UsGroupPermission>();

    public virtual ICollection<UsUserPermission> UsUserPermissions { get; set; } = new List<UsUserPermission>();
}
