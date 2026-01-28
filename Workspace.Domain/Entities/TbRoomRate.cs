using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace_Management_System.Entities;

namespace Workspace.Domain.Entities
{
    public partial class TbRoomRate
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "please enter the room")]
        public long RoomId { get; set; }
        [Required(ErrorMessage = "please enter the mode")]
        public string Mode { get; set; }
        [Required(ErrorMessage = "please enter the hourly rate")]
        public decimal HourlyRate { get; set; }

        public virtual TbRoom Room { get; set; } = null!;
    }
}
