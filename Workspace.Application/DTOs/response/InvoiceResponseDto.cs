using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.response
{
    public class InvoiceResponseDto
    {
        public long Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string UserId { get; set; }
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
        public string Status { get; set; }
        public string? Notes { get; set; }
        public List<InvoiceBookingItemDto> Bookings { get; set; }
    }

    public class InvoiceBookingItemDto
    {
        public long BookingId { get; set; }
        public decimal BookingRoomPrice { get; set; }
        public decimal BookingProductPrice { get; set; }
        public decimal BookingTotal { get; set; }
    }
}
