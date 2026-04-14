using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Ordersproduct
{
    public int IdOrderProduct { get; set; }

    public int? Order { get; set; }

    public int? Product { get; set; }

    public int? Count { get; set; }

    public virtual Order? OrderNavigation { get; set; }

    public virtual Product? ProductNavigation { get; set; }
}
