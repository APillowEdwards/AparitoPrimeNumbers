using Microsoft.Extensions.Caching.Memory;
using PrimeNumbersAPI.Responses;
using PrimeNumbersAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumbersAPI.Logic
{
    public class PrimeNumberLogic : IPrimeNumberLogic
    {
        private readonly int SMALLEST_PRIME = 2;

        private IMemoryCache _isPrimeCache;

        public PrimeNumberLogic(IMemoryCache cache)
        {
            _isPrimeCache = cache;
        }

        public APIResponse<IEnumerable<int>> GetPrimesMax(int max)
        {
            IEnumerable<int> primeNumbers = GetPrimesMaxValues(max);

            return APIResponse<IEnumerable<int>>.OkResponse(primeNumbers);
        }

        public APIResponse<PrimeNumberPage> GetPrimesMaxWithPages(int max, int pageSize, int pageIndex)
        {
            if (pageSize < 1)
            {
                return APIResponse<PrimeNumberPage>.BadRequestResponse("Page size cannot be less than 1");
            }

            if (pageIndex < 0)
            {
                return APIResponse<PrimeNumberPage>.BadRequestResponse("Page index cannot be less than 0");
            }

            IEnumerable<int> primeNumbers = GetPrimesMaxValues(max);

            PrimeNumberPage page = new PrimeNumberPage
            {
                PrimeNumbers = primeNumbers
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize),

                NumberOfPages = (int)Math.Ceiling((decimal) primeNumbers.Count() / (decimal)pageSize)
            };

            return APIResponse<PrimeNumberPage>.OkResponse(page);
        }

        private IEnumerable<int> GetPrimesMaxValues(int max)
        {
            /*
             * Using "max - 1" here as we want to start with 2 (because 1 looks prime but isn't) 
             * and we don't want to include the number above "max".
             */
            IEnumerable<int> numbersToCheck = Enumerable.Range(SMALLEST_PRIME, max - 1);
            List<int> primeNumbers = new List<int>();

            foreach (int number in numbersToCheck)
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            }

            return primeNumbers;
        }

        public bool IsPrime(int number)
        {
            if (number <= 1) // numbers below 2 are never prime
            {
                return false;
            }

            // Check the cache and return the value if it's already been calculated

            bool isPrime;
            if (_isPrimeCache.TryGetValue(number, out isPrime))
            {
                return isPrime;
            }

            isPrime = true;

            //Since having 1 as a factor doesn't make a number not prime, we need to start from 2 and work up
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) // if "i" is a factor of "number" 
                {
                    isPrime = false;
                    break;
                }
            }

            // Cache the value so it won't have to be recalculated later
            _isPrimeCache.Set(number, isPrime);

            return isPrime;
        }
    }
}
