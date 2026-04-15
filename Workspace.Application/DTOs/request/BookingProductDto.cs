using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class BookingProductDto
    {
        [Required(ErrorMessage = "please enter the booking")]
        public long BookingId { get; set; }
        [Required(ErrorMessage = "please enter the product")]

        public long ProductId { get; set; }
        [Required(ErrorMessage = "please enter the product qty")]
        public int ProductQty { get; set; }
    }
}
