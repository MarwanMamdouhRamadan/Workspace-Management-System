using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class CreateInvoiceDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public List<long> BookingIds { get; set; }

        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; } = 0;

        [Range(0, 100)]
        public decimal TaxPercentage { get; set; } = 0;

        public string? Notes { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}
