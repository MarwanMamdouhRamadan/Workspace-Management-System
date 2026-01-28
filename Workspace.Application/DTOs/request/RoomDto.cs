using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class RoomDto
    {
        [Required(ErrorMessage = "Please enter the room name")]
        public string RoomName { get; set; } = null!;
        [Required(ErrorMessage = "Please enter the room capacity")]
        public int Capacity { get; set; }
    }
}
