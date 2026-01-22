using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbSetting
{
    public long Id { get; set; }

    public string KeyName { get; set; } = null!;

    public string KeyValue { get; set; } = null!;

    public string? Description { get; set; }
}
