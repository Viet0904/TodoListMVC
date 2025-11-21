using System;
using System.Collections.Generic;

namespace TodoListMVC.Data.Entities;

public partial class UsUserLog
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Menu { get; set; }

    public string ComputerName { get; set; }

    public string ActionName { get; set; }

    public string Data { get; set; }

    public string Note { get; set; }

    public DateTime ActionDate { get; set; }

    public virtual UsUser User { get; set; }
}
