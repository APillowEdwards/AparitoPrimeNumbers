using Microsoft.Extensions.Caching.Memory;
using PrimeNumbersAPI.Logic;
using PrimeNumbersAPI.Responses;
using PrimeNumbersAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PrimeNumbersAPITest
{
    public class PrimeNumberTest
    {
        private IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        [Fact]
        public void PrimeNumber_IsPrime_NegativeNumbersArentPrime()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            Assert.False(pn.IsPrime(-1));
            Assert.False(pn.IsPrime(-5));
        }

        [Fact]
        public void PrimeNumber_IsPrime_OneIsntPrime()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            Assert.False(pn.IsPrime(1));
        }

        [Fact]
        public void PrimeNumber_IsPrime_SmallPrimes()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            Assert.True(pn.IsPrime(2));
            Assert.True(pn.IsPrime(3));
            Assert.True(pn.IsPrime(5));
            Assert.True(pn.IsPrime(7));
        }

        [Fact]
        public void PrimeNumber_IsPrime_LargePrime()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            Assert.True(pn.IsPrime(16785407));
        }

        [Fact]
        public void PrimeNumber_IsPrime()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            Assert.True(pn.IsPrime(16785407));
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualTo_OneShouldBeEmpty()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            IEnumerable<int> primes = pn.GetPrimesBelowOrEqualTo(1).Result;

            Assert.Empty(primes);
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualTo_2ShouldBeJust2()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            IEnumerable<int> primes = pn.GetPrimesBelowOrEqualTo(2).Result;

            Assert.Single(primes);
            Assert.Equal(2, primes.FirstOrDefault());
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualTo_SmallPrimes()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            IEnumerable<int> primes = pn.GetPrimesBelowOrEqualTo(10).Result;
            List<int> expectedPrimes = new List<int>
            {
                2, 3, 5, 7
            };

            Assert.Equal(4, primes.Count());
            Assert.Equal(expectedPrimes, primes);
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualTo_500()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            IEnumerable<int> primes = pn.GetPrimesBelowOrEqualTo(500).Result;

            Assert.Equal(95, primes.Count());
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualToWithPages_500FirstPage()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            PrimeNumberPage page = pn.GetPrimesBelowOrEqualToWithPages(500, 5, 0).Result;

            List<int> expectedPrimes = new List<int>
            {
                2, 3, 5, 7, 11
            };

            Assert.Equal(5, page.PrimeNumbers.Count());
            Assert.Equal(expectedPrimes, page.PrimeNumbers);

            Assert.Equal(95 / 5, page.NumberOfPages);
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualToWithPages_500SecondPage()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            PrimeNumberPage page = pn.GetPrimesBelowOrEqualToWithPages(500, 5, 1).Result;

            List<int> expectedPrimes = new List<int>
            {
                13, 17, 19, 23, 29
            };

            Assert.Equal(5, page.PrimeNumbers.Count());
            Assert.Equal(expectedPrimes, page.PrimeNumbers);

            Assert.Equal(95 / 5, page.NumberOfPages);
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualToWithPages_500WithCaching()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            PrimeNumberPage page = pn.GetPrimesBelowOrEqualToWithPages(500, 5, 0).Result;
            page = pn.GetPrimesBelowOrEqualToWithPages(500, 5, 0).Result;

            List<int> expectedPrimes = new List<int>
            {
                2, 3, 5, 7, 11
            };

            Assert.Equal(5, page.PrimeNumbers.Count());
            Assert.Equal(expectedPrimes, page.PrimeNumbers);

            Assert.Equal(95 / 5, page.NumberOfPages);
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualToWithPages_PageSize0()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            int code = pn.GetPrimesBelowOrEqualToWithPages(500, 0, 0).Code;

            Assert.Equal(400, code);
        }

        [Fact]
        public void PrimeNumber_GetPrimesBelowOrEqualToWithPages_PageIndexLessThan0()
        {
            PrimeNumberLogic pn = new PrimeNumberLogic(_cache);

            int code = pn.GetPrimesBelowOrEqualToWithPages(500, 5, -1).Code;

            Assert.Equal(400, code);
        }
    }
}
