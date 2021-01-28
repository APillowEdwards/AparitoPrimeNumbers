using PrimeNumbersAPI.Responses;
using PrimeNumbersAPI.ViewModels;
using System.Collections.Generic;

namespace PrimeNumbersAPI.Logic
{
    public interface IPrimeNumberLogic
    {
        APIResponse<IEnumerable<int>> GetPrimesBelowOrEqualTo(int max);
        APIResponse<PrimeNumberPage> GetPrimesBelowOrEqualToWithPages(int max, int pageSize, int pageIndex);
        bool IsPrime(int number);
    }
}