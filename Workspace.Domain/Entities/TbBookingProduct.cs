using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbBookingProduct
{
    public long BookingId { get; set; }

    public long ProductId { get; set; }

    public int ProductQty { get; set; }

    public decimal ProductUnitPrice { get; set; }

    public decimal ProductTotalPrice { get; set; }

    public virtual TbBooking Booking { get; set; } = null!;

    public virtual TbProduct Product { get; set; } = null!;
}
