using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
            var customer = _context.Customers.Include(x => x.MembershipType).SingleOrDefault(c => c.Id == rentalDto.CustomerId);
            var movie = _context.Movies.Include(x => x.Genre).SingleOrDefault(c => c.Id == rentalDto.MovieId);
            movie.NumberAvailable -= 1;
            rental.Customer = customer;
            rental.Movie = movie;
            rental.Sum = movie.Genre.Price * (100 - customer.MembershipType.DiscountRate) / 100;
            _context.Rentals.Add(rental);
            _context.SaveChanges();
            return RedirectToAction("Index", "Rentals");
        }

        public ActionResult Details(int id)
        {
            var rental = _context.Rentals.Include(c => c.Customer).Include(x => x.Movie).SingleOrDefault(c => c.Id == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }

        public ActionResult Return(int id)
        {
            var rental = _context.Rentals.Include(c => c.Customer).Include(x => x.Movie).SingleOrDefault(c => c.Id == id);

            if (rental == null)
            {
                return NotFound();
            }
            rental.ReturnedOn = DateTime.Now;
            rental.Returned = true;
            rental.Movie.NumberAvailable += 1;
            _context.SaveChanges();

            return View("Details", rental);
        }
    }
}
