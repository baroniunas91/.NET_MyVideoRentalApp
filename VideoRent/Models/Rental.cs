using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRent.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime RentedOn { get; set; } = DateTime.Now;
        public bool Returned { get; set; } = false;
        public DateTime? ReturnedOn { get; set; }
        public Movie Movie { get; set; }
        public Customer Customer { get; set; }
        public RentalStatus RentalStatus { get; set; }
        public int? RentalStatusId { get; set; }
    }
}