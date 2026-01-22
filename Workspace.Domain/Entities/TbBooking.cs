using System;
using System.Collections.Generic;
using Workspace_Managment_System.identity;

namespace Workspace_Management_System.Entities;

public partial class TbBooking
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;

    public long RoomId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal RoomPrice { get; set; }

    public long StatusId { get; set; }

    public int CountOfPeople { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual TbRoom Room { get; set; } = null!;

    public virtual TbStatus Status { get; set; } = null!;

    public virtual ICollection<TbBookingProduct> TbBookingProducts { get; set; } = new List<TbBookingProduct>();

    public virtual TbInvoiceBooking? TbInvoiceBooking { get; set; }
}
