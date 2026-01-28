using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workspace.Domain.Entities;

namespace Workspace_Management_System.Entities;

public partial class TbRoom
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Please enter the room name")]
    public string RoomName { get; set; } = null!;
    [Required(ErrorMessage = "Please enter the room capacity")]
    public int Capacity { get; set; }
    [Required(ErrorMessage = "Please enter the status")]
    [ForeignKey("Status")]
    public long StatusId { get; set; }
    public virtual TbStatus Status { get; set; } = null!;

    public virtual ICollection<TbRoomRate> RoomRates { get; set; } = new List<TbRoomRate>();

    public virtual ICollection<TbBooking> TbBookings { get; set; } = new List<TbBooking>();
    public virtual ICollection<TbMedium> TbMedia { get; set; } = new List<TbMedium>();
}
