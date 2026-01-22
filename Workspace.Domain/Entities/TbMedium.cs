using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbMedium
{
    public long Id { get; set; }

    public string RelativePathFile { get; set; } = null!;

    public long RoomId { get; set; }

    public virtual TbRoom Room { get; set; } = null!;
}
