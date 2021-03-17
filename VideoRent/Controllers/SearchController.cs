using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.ViewModels;

namespace VideoRent.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index(SearchViewModel searchViewModel)
        {
            return View();
        }
    }
}
