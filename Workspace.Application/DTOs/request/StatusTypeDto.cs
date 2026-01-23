using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class StatusTypeDto
    {
        [Required(ErrorMessage ="please enter the type name")]
        public string TypeName { get; set; }
    }
}
