using System.ComponentModel.DataAnnotations;

namespace VideoRent.Models
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "Please select a customer")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
    }
}
