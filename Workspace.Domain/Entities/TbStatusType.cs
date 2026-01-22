using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbStatusType
{
    public long Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<TbStatus> TbStatuses { get; set; } = new List<TbStatus>();
}
