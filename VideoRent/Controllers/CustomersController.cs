using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Data;
using VideoRent.Functions;
using VideoRent.Models;
using VideoRent.ViewModels;

namespace VideoRent.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).OrderBy(x => x.Name).ToList();

            return View(customers);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewmodel);
            }

            if (customer.Id == 0)
            {
                FunctionsSP.SaveNewCustomer(_context, customer);

                return RedirectToAction("Index", "Customers");
            }
            else
            {
                var customerInDb = _context.Customers.Include(c => c.MembershipType).Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                _context.SaveChanges();

                return RedirectToAction("Details", "Customers", new { id = customerInDb.Id });
            }
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Details(int id, bool? delErr)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            if (delErr ?? false)
                ModelState.AddModelError("HasUnreturnedRentals", "The custumer has unreturned videos.");

            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            if (_context.Rentals.Include(r => r.Customer).Where(r => r.Customer.Id == customer.Id && !r.Returned).Any())
            {
                return RedirectToAction("Details", new { id, delErr = true });
            }

            var dependantRentals = _context.Rentals.Include(r => r.Customer).Where(r => r.Customer.Id == customer.Id);
            _context.RemoveRange(dependantRentals);

            _context.Customers.Remove(customer);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult History(int id)
        {
            var history = _context.Rentals.Include(c => c.Customer).Include(x => x.Movie).Where(x => x.Customer.Id == id).ToList();
            var customer = _context.Customers.Where(x => x.Id == id).SingleOrDefault();
            
            var viewModel = new CustomerHistoryViewModel()
            {
                Customer = customer,
                CustomerRentals = history
            };

            return View("History", viewModel);
        }

        public ActionResult ExportExcel()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).OrderBy(x => x.Id).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Customers");
                var currentRow = 1;

                #region Header
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Customer Name";
                worksheet.Cell(currentRow, 3).Value = "Discount Rate";
                worksheet.Cell(currentRow, 4).Value = "Membership Type";
                #endregion

                #region Body
                foreach (var customer in customers)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = customer.Id;
                    worksheet.Cell(currentRow, 2).Value = customer.Name;
                    worksheet.Cell(currentRow, 3).Value = customer.MembershipType.DiscountRate;
                    worksheet.Cell(currentRow, 4).Value = customer.MembershipType.Name;
                }
                #endregion

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Customers.xlsx"
                        );
                }
            }
        }
    }
}
