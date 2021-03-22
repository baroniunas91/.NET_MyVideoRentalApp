using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRent.Models;

namespace VideoRent.ViewModels
{
    public class CustomerHistoryViewModel
    {
        [Required]
        public IEnumerable<Rental> CustomerRentals { get; set; }
        [Required]
        public Customer Customer { get; set; }
    }
}
