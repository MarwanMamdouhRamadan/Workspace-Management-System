using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbInvoiceBooking
{
    public long InvoiceId { get; set; }

    public long BookingId { get; set; }

    public decimal BookingRoomPrice { get; set; }

    public decimal BookingProductPrice { get; set; }

    public decimal BookingTotal { get; set; }

    public virtual TbBooking Booking { get; set; } = null!;

    public virtual TbInvoice Invoice { get; set; } = null!;
}
