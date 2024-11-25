using System;
using System.Collections.Generic;

namespace ModAppEv.Models;

public partial class ProductChangeLog
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string TableAction { get; set; } = null!;

    public string ChangeBy { get; set; } = null!;

    public DateTime ChangeDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
