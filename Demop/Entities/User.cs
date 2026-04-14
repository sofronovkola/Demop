using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class User
{
    public int IdUser { get; set; }

    public int? RoleEmployee { get; set; }

    public string? Fio { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? RoleEmployeeNavigation { get; set; }
}
