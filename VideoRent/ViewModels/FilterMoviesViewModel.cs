using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Models;
using VideoRent.Utilities;

namespace VideoRent.ViewModels
{
    public class FilterMoviesViewModel
    {
        [Display(Name = "Genre")]
        public IEnumerable<Genre> Genres { get; set; }
        public int? GenreId { get; set; }
        [Display(Name = "Movie")]
        public IEnumerable<Movie> Movies { get; set; }
        public int? MovieId { get; set; }
        public IEnumerable<Rental> FoundRentals { get; set; }
        [Display(Name = "Text")]
        public string Input { get; set; }
    }
}
