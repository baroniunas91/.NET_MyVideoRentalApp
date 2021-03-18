using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Data;
using VideoRent.Models;
using VideoRent.ViewModels;

namespace VideoRent.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).OrderBy(m => m.Genre.Name).ToList();

            return View(movies);
        }

        public IActionResult New()
        {
            var movieFormViewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = _context.Genres.ToList(),
                TitleMessage = "New Movie"
            };

            return View("MovieForm", movieFormViewModel);
        }

        [HttpPost]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList(),
                    TitleMessage = TempData["TitleMessage"].ToString()
                };
                return View("MovieForm", viewModel);
            }

            if(movie.Id == 0)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }

            var movieInDb = _context.Movies.Include(m => m.Genre).Single(m => m.Id == movie.Id);

            movieInDb.Name = movie.Name;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.NumberInStock = movie.NumberInStock;
            movieInDb.NumberAvailable = movie.NumberAvailable;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.DateAdded = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Details", "Movies", new { Id = movie.Id });         
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return NotFound();
            }

            var movieFormViewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList(),
                TitleMessage = "Edit Movie"
            };

            return View("MovieForm", movieFormViewModel);
        }
    }
}
