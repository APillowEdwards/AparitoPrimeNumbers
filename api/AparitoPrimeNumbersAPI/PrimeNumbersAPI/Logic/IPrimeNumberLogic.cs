using PrimeNumbersAPI.Responses;
using PrimeNumbersAPI.ViewModels;
using System.Collections.Generic;

namespace PrimeNumbersAPI.Logic
{
    public interface IPrimeNumberLogic
    {
        APIResponse<IEnumerable<int>> GetPrimesMax(int max);
        APIResponse<PrimeNumberPage> GetPrimesMaxWithPages(int max, int pageSize, int pageIndex);
        bool IsPrime(int number);
    }
}