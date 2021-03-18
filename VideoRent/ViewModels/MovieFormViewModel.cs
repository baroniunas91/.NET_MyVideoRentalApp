using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Models;

namespace VideoRent.ViewModels
{
    public class MovieFormViewModel
    {
        [Required]
        public IEnumerable<Genre> Genres { get; set; }
        [Required]
        public Movie Movie { get; set; }
        public string TitleMessage { get; set; }
    }
}
