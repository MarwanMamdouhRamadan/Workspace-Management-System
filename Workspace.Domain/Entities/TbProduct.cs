using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbProduct
{
    public long Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<TbBookingProduct> TbBookingProducts { get; set; } = new List<TbBookingProduct>();
}
