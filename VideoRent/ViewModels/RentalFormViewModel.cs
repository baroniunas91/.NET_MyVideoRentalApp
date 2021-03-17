using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoRent.Models;

namespace VideoRent.ViewModels
{
    public class RentalFormViewModel
    {
        [Required]
        public IEnumerable<Movie> Movies { get; set; }
        [Required]
        public IEnumerable<Customer> Customers { get; set; }
        [Required]
        public RentalDto RentalDto { get; set; }
    }
}
