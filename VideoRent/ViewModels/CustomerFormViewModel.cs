using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Models;

namespace VideoRent.ViewModels
{
    public class CustomerFormViewModel
    {
        [Required]
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        [Required]
        public Customer Customer { get; set; }

    }
}
