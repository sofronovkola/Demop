using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Manufacturer
{
    public int IdManufacturer { get; set; }

    public string? Manufacturer1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
