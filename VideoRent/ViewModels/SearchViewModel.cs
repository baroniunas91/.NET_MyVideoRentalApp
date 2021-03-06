using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Models;

namespace VideoRent.ViewModels
{
    public class SearchViewModel
    {
        public string CustomerName { get; set; }
        public string MovieName { get; set; }
        [Display(Name = "Search by Name")]
        public bool SearchByName { get; set; }
        [Display(Name = "Search by Genre")]
        public bool SearchByGenre { get; set; }
        public List<Customer> FoundCustomers { get; set; }
        public List<Movie> FoundMovies { get; set; }

    }
}
