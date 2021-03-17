using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRent.Models
{
    public class RentalDto
    {
        [Required(ErrorMessage = "Please select a customer")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please select a movie")]
        [Display(Name = "Movie")]
        public int MovieId { get; set; }
    }
}
