using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumbersAPI.ViewModels
{
    public class PrimeNumberPage
    {
        public IEnumerable<int> PrimeNumbers { get; set; }
        public int NumberOfPages { get; set; }
    }
}
