using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbStatus
{
    public long Id { get; set; }

    public string StatusName { get; set; } = null!;

    public long StatusTypeId { get; set; }

    public virtual TbStatusType StatusType { get; set; } = null!;
    public virtual TbProduct Product { get; set; } = null!;
    public virtual TbRoom Room { get; set; } = null!;

    public virtual ICollection<TbBooking> TbBookings { get; set; } = new List<TbBooking>();

    public virtual ICollection<TbInvoice> TbInvoices { get; set; } = new List<TbInvoice>();
}
