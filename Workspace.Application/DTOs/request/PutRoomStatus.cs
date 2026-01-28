using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class PutRoomStatus
    {
        [Required(ErrorMessage = "please enter product")]
        public long roomId { get; set; }
        [Required(ErrorMessage = "please enter status")]
        public long statusId { get; set; }
    }
}
