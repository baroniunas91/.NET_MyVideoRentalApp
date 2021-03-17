using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Data;
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
                //todo
            }
            return View();
        }
    }
}
