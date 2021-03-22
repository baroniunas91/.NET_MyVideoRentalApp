using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Models;

namespace VideoRent.ViewModels
{
    public class FilterRentalsViewModel
    {
        public IEnumerable<Movie> AllMovies { get; set; }
        [Display(Name = "Movie")]
        public int MovieId { get; set; }
        public IEnumerable<Rental> FilteredRentals { get; set; }
        [Display(Name = "Rental Status")]
        public IEnumerable<RentalStatus> RentalStatuses { get; set; }
        public int RentalStatusId { get; set; }
    }
}
