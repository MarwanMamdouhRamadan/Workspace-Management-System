using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.response
{
    public class StatusDtoResponse
    {
        [Required(ErrorMessage = "please enter the status name")]
        public string StatusName { get; set; } = null!;
    }
}
