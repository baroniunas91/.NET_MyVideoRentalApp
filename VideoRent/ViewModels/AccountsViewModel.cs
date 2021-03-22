using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRent.Models;

namespace VideoRent.ViewModels
{
    public class AccountsViewModel
    {
        [Required]
        public IEnumerable<Customer> Customers { get; set; }
        [Required]
        public IEnumerable<Rental> Rentals { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public CustomerDto CustomerDto { get; set; }
        [Required]
        public decimal RentalsTotalSum { get; set; }
    }
}
