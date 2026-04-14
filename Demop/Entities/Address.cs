using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Address
{
    public int IdAddress { get; set; }

    public string? AddressName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
