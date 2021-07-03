using CustomerAPI.Models;
using CustomerAPI.Models.Exceptions;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerCacheService _customerCacheService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerCacheService customerCacheService)
        {
            _logger = logger;
            _customerCacheService = customerCacheService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _customerCacheService.GetAll();
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody] List<Customer> request)
        {
            try
            {
                _logger.LogInformation($"Add Customer: {request.ToString()}");

                await _customerCacheService.AddRangeAsync(request);

                return Ok();
            }
            catch (ApiException ex)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error");
        }
    }
}
