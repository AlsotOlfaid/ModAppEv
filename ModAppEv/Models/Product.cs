using System;
using System.Collections.Generic;

namespace ModAppEv.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int SupplierId { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public double? Discount { get; set; }

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual ICollection<ProductChangeLog> ProductChangeLogs { get; set; } = new List<ProductChangeLog>();

    public virtual Supplier Supplier { get; set; } = null!;
}
