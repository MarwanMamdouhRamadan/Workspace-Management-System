using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.DTOs.request
{
    public class ProductDto
    {
        [Required(ErrorMessage ="please enter the product name")]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "please enter the price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "please enter the stock")]
        public int Stock { get; set; }
    }
}
