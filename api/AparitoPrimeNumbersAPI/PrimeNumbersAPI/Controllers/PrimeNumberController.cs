using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeNumbersAPI.Logic;
using PrimeNumbersAPI.Responses;
using PrimeNumbersAPI.ViewModels;

namespace PrimeNumbersAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimeNumberController : ControllerBase
    {
        private readonly ILogger<PrimeNumberController> _logger;
        private readonly IPrimeNumberLogic _primeNumberLogic;

        public PrimeNumberController(ILogger<PrimeNumberController> logger, IPrimeNumberLogic primeNumberLogic)
        {
            _logger = logger;
            _primeNumberLogic = primeNumberLogic;
        }

        [HttpGet("{max}")]
        public APIResponse<IEnumerable<int>> GetPrimesBelowOrEqualTo(int max)
        {
            try
            {
                return _primeNumberLogic.GetPrimesBelowOrEqualTo(max);
            }
            catch (Exception e)
            {
                _logger.LogError("Unhandled exception: " + e.Message);
                return APIResponse<IEnumerable<int>>.UnhandledExceptionResponse();
            }
        }

        [HttpGet("{max}/{pageSize}/{pageNumber}")]
        public APIResponse<PrimeNumberPage> GetPrimesBelowOrEqualTo(int max, int pageSize, int pageIndex)
        {
            try
            {
                return _primeNumberLogic.GetPrimesBelowOrEqualToWithPages(max, pageSize, pageIndex);
            }
            catch (Exception e)
            {
                _logger.LogError("Unhandled exception: " + e.Message);
                return APIResponse<PrimeNumberPage>.UnhandledExceptionResponse();
            }
        }
    }
}
