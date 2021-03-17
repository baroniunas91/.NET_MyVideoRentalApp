using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoRent.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter movie's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter movie's release date.")]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please select movie's genre.")]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Required(ErrorMessage = "Please select movie's number in stock.")]
        [Display(Name = "Number in Stock")]
        public int? NumberInStock { get; set; } = 0;

        [Required(ErrorMessage = "Please select number of available movies.")]
        [Display(Name = "Number Available")]
        public int? NumberAvailable { get; set; } = 0;

        public Genre Genre { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
