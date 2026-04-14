using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Namesproduct
{
    public int IdNameproduct { get; set; }

    public string? NameProduct { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
