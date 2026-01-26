using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workspace_Management_System.Entities;

public partial class TbProduct
{
    public long Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }
    [ForeignKey("tbStatus")]
    public long StatusId { get; set; }
    public TbStatus tbStatus { get; set; }

    public virtual ICollection<TbBookingProduct> TbBookingProducts { get; set; } = new List<TbBookingProduct>();
}
