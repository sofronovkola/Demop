using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Suplier
{
    public int IdSuplier { get; set; }

    public string? Suplier1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
