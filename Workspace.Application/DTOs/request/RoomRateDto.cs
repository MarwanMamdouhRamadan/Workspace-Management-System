using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class RoomRateDto
    {
        [Required(ErrorMessage = "please enter the room")]
        public long RoomId { get; set; }
        [Required(ErrorMessage = "please enter the mode")]
        public string Mode { get; set; }
        [Required(ErrorMessage = "please enter the hourly rate")]
        public decimal HourlyRate { get; set; }
    }
}
