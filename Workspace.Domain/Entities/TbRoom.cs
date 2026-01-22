using System;
using System.Collections.Generic;

namespace Workspace_Management_System.Entities;

public partial class TbRoom
{
    public long Id { get; set; }

    public string RoomName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Capacity { get; set; }

    public long PricingTypeId { get; set; }

    public virtual TbPricingType PricingType { get; set; } = null!;

    public virtual ICollection<TbBooking> TbBookings { get; set; } = new List<TbBooking>();

    public virtual ICollection<TbMedium> TbMedia { get; set; } = new List<TbMedium>();
}
