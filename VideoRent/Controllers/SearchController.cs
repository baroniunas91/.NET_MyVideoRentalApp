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
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index(SearchViewModel searchViewModel, string searchButton)
        {
            if (searchButton == "customer"){
                var customers = _context.Customers.Where(c => c.Name.Contains(searchViewModel.CustomerName));

                searchViewModel.FoundCustomers = customers.ToList();

                return View(searchViewModel);
            }
            else if(searchButton == "movie")
            {
                if(!searchViewModel.SearchByName && !searchViewModel.SearchByGenre)
                {
                    ModelState.AddModelError("OneOfCheckBoxesChecked", "Please select at least one search criteria");
                    return View(searchViewModel);
                }

                var movies = new HashSet<Movie>();

                if (searchViewModel.SearchByName)
                {
                    movies.UnionWith(_context.Movies.Where(m => m.Name.Contains(searchViewModel.MovieName)).ToList());
                }
                if (searchViewModel.SearchByGenre)
                {
                    movies.UnionWith(_context.Movies.Include(m => m.Genre).Where(m => m.Genre.Name.Contains(searchViewModel.MovieName)).ToList());
                }

                searchViewModel.FoundMovies = movies.ToList();

                return View(searchViewModel);
            }
            return View(searchViewModel);
        }
    }
}
