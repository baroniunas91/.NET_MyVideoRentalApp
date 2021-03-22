using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRent.Models;

namespace VideoRent.Services
{
    public class AccountService
    {
        public decimal CountTotalRentalsSum(List<Rental> rentals)
        {
            decimal totalSum = 0;
            foreach (var rental in rentals)
            {
                totalSum += rental.Sum;
            }
            return totalSum;
        }
    }
}
