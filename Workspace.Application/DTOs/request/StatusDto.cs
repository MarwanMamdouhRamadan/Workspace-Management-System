using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class StatusDto
    {
        [Required(ErrorMessage = "please enter the status name")]
        public string StatusName { get; set; } = null!;
        [Required(ErrorMessage = "please enter the status type")]

        public long StatusTypeId { get; set; }
    }
}
