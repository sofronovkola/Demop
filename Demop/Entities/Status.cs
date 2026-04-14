using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Status
{
    public int IdStatus { get; set; }

    public string? Status1 { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
