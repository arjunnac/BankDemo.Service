using BankDemo.AccountService.DataModel;
using BankDemo.AccountService.Service;
using BankDemo.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankDemo.AccountService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private ITransactionService _transactionService;

        public CustomerController(ILogger<CustomerController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpPost("Add", Name = "AddCustomers")]
        public IActionResult Post([FromBody] Customer customer)
        {
            var reply = _transactionService.AddCustomer(customer);
            return Ok(new ResponseMessage(reply.Result.status, reply.Result.message));
        }

        [HttpGet("All", Name = "GetAllCustomerDetails")]
        public IActionResult GetAllCustomers()
        {
            var customers = _transactionService.GetAllCustomers();
            if (customers?.Result != null)
                return Ok(customers?.Result);
            else
                return NoContent();
        }

        [HttpGet("{customerId}", Name = "GetCustomerDetails")]
        public IActionResult GetAllCustomers(string customerId)
        {
            var customers = _transactionService.GetAccountDetailsOfCustomerAsync(customerId);
            if (customers?.Result != null)
                return Ok(customers?.Result);
            else
                return NoContent();
        }
    }
}
