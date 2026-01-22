using System;
using System.Collections.Generic;
using Workspace_Managment_System.identity;

namespace Workspace_Management_System.Entities;

public partial class TbInvoice
{
    public long Id { get; set; }

    public string InvoiceNumber { get; set; } = null!;
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;
    public string CreatedBy { get; set; }
    public string UpdtedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime InvoiceDate { get; set; }

    public decimal TotalRoomPrice { get; set; }

    public decimal TotalProductPrice { get; set; }

    public decimal SubTotal { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TotalAfterDiscount { get; set; }

    public decimal TaxPercentage { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public long StatusId { get; set; }

    public string? Notes { get; set; }

    public virtual TbStatus Status { get; set; } = null!;

    public virtual ICollection<TbInvoiceBooking> TbInvoiceBookings { get; set; } = new List<TbInvoiceBooking>();
}
