using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class UsUserPermission
{
    public string UserId { get; set; }

    public string MenuId { get; set; }

    public bool Xem { get; set; }

    public bool Them { get; set; }

    public bool Sua { get; set; }

    public bool SuaHangLoat { get; set; }

    public bool Xoa { get; set; }

    public bool XoaHangLoat { get; set; }

    public bool XuatDuLieu { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual SysMenu Menu { get; set; }

    public virtual UsUser User { get; set; }
}
