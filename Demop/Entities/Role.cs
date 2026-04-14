using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Role
{
    public int IdRole { get; set; }

    public string? RoleEmployee { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
