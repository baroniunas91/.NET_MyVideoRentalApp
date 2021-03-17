using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VideoRent.Data;
using VideoRent.Models;
using VideoRent.ViewModels;

namespace VideoRent.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            var rentalsList = _context.Rentals.Include(c => c.Customer).Include(x => x.Movie).OrderBy(x => x.Id).ToList();
            return View(rentalsList);
        }

        public ActionResult New()
        {
            var customers = _context.Customers.OrderBy(x => x.Name).ToList();
            var movies = _context.Movies.OrderBy(x => x.Name).Where(x => x.NumberAvailable >= 1).ToList();
            var viewModel = new RentalFormViewModel
            {
                Customers = customers,
                Movies = movies,
                RentalDto = new RentalDto()
            };
            return View("RentalForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new RentalFormViewModel
                {
                    Customers = _context.Customers.OrderBy(x => x.Name).ToList(),
                    Movies = _context.Movies.OrderBy(x => x.Name).ToList(),
                    RentalDto = rentalDto
                };
                return View("RentalForm", viewmodel);
            }

            var rental = new Rental();
            var customer = _context.Customers.SingleOrDefault(c => c.Id == rentalDto.CustomerId);
            var movie = _context.Movies.SingleOrDefault(c => c.Id == rentalDto.MovieId);
            movie.NumberAvailable -= 1;
            rental.Customer = customer;
            rental.Movie = movie;
            _context.Rentals.Add(rental);
            _context.SaveChanges();
            return RedirectToAction("Index", "Rentals");
        }
    }
}
