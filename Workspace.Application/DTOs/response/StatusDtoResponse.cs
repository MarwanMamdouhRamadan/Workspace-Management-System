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
        public long id {  get; set; }
        public string StatusName { get; set; } = null!;
        public long StatusTypeId { get; set; }
    }
}
