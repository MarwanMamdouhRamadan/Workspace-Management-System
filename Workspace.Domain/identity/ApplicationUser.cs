using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace Workspace_Managment_System.identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="please enter your name")]
        public string FullName {  get; set; }
    }
}
