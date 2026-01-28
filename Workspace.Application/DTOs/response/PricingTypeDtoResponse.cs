using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.response
{
    public class PricingTypeDtoResponse
    {
        public long Id { get; set; }

        public string TypeName { get; set; } = null!;

        public int DurationMinutes { get; set; }
    }
}
