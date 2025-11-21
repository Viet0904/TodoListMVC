using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class UsUser
{
    public string Id { get; set; }

    public string GroupId { get; set; }

    public string Name { get; set; }

    public int? Gender { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string CMND { get; set; }

    public string Address { get; set; }

    public byte[] Image { get; set; }

    public string Note { get; set; }

    public bool RowStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual UsGroup Group { get; set; }

    public virtual ICollection<TDCategory> TDCategories { get; set; } = new List<TDCategory>();

    public virtual ICollection<TDTask> TDTaskCreatedByNavigations { get; set; } = new List<TDTask>();

    public virtual ICollection<TDTask> TDTaskUpdatedByNavigations { get; set; } = new List<TDTask>();

    public virtual ICollection<TDTask> TDTaskUsers { get; set; } = new List<TDTask>();

    public virtual ICollection<UsUserLog> UsUserLogs { get; set; } = new List<UsUserLog>();

    public virtual ICollection<UsUserPermission> UsUserPermissions { get; set; } = new List<UsUserPermission>();
}
