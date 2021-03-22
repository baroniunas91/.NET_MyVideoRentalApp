using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VideoRent.Data;
using VideoRent.Models;
using VideoRent.Services;
using VideoRent.ViewModels;

namespace VideoRent.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationDbContext _context;
        private AccountService _accountService;

        public AccountsController(ApplicationDbContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).OrderBy(x => x.Name).ToList();
            
            if (customers == null)
            {
                return NotFound();
            }
            
            var viewModel = new AccountsViewModel
            {
                Customers = customers,
                CustomerDto = new CustomerDto()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Show(CustomerDto customerDto)
        {
            var customers = _context.Customers.Include(c => c.MembershipType).OrderBy(x => x.Name).ToList();
            if (!ModelState.IsValid)
            {
                var viewModel = new AccountsViewModel
                {
                    Customers = customers,
                    CustomerDto = customerDto
                };
                return View("Index", viewModel);
            }
            else
            {
                var showCustomer = _context.Customers.Include(c => c.MembershipType).Where(x => x.Id == customerDto.CustomerId).SingleOrDefault();
                var rentals = _context.Rentals.Include(c => c.Customer).Include(x => x.Movie).ThenInclude(x => x.Genre).OrderBy(x => x.Id).Where(x => x.Customer.Id == customerDto.CustomerId).ToList();
                var rentalsTotalSum = _accountService.CountTotalRentalsSum(rentals);
                var viewModel = new AccountsViewModel
                {
                    Customers = customers,
                    Customer = showCustomer,
                    Rentals = rentals,
                    RentalsTotalSum = rentalsTotalSum,
                    CustomerDto = customerDto
                };
                return View("Index", viewModel);
            }
            //var rental = new Rental();
            //var customer = _context.Customers.SingleOrDefault(c => c.Id == rentalDto.CustomerId);
            //var movie = _context.Movies.SingleOrDefault(c => c.Id == rentalDto.MovieId);
            //movie.NumberAvailable -= 1;
            //rental.Customer = customer;
            //rental.Movie = movie;
            //_context.Rentals.Add(rental);
            //_context.SaveChanges();
            //return RedirectToAction("Index", "Rentals");
        }

    }
}
