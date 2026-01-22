using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="please enter your name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "please enter your email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "please enter your password")]
        public string Password { get; set; }
    }
}
