using System;
using System.Collections.Generic;

namespace Demop.Entities;

public partial class Order
{
    public int IdOrder { get; set; }

    public int? Number { get; set; }

    public int? Article { get; set; }

    public int? Count { get; set; }

    public string? DateOrder { get; set; }

    public string? DateDelivery { get; set; }

    public int? Address { get; set; }

    public int? IdUser { get; set; }

    public int? CodeToReceive { get; set; }

    public int? Status { get; set; }

    public virtual Address? AddressNavigation { get; set; }

    public virtual Product? ArticleNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Ordersproduct> Ordersproducts { get; set; } = new List<Ordersproduct>();

    public virtual Status? StatusNavigation { get; set; }
}
