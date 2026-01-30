using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.response
{
    public class RoomRateResponseDto
    {
        [Required(ErrorMessage = "please enter the room")]
        public string RoomName { get; set; }
        [Required(ErrorMessage = "please enter the mode")]
        public string Mode { get; set; }
        [Required(ErrorMessage = "please enter the hourly rate")]
        public decimal HourlyRate { get; set; }
    }
}
