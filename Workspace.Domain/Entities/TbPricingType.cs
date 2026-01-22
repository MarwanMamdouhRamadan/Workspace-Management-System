using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbPricingType
{
    public long Id { get; set; }

    public string TypeName { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public virtual ICollection<TbRoom> TbRooms { get; set; } = new List<TbRoom>();
}
