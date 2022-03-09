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
        /// <summary>
        /// This API method is used to add new customer account to the database.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("Add", Name = "AddCustomers")]
        public IActionResult Post([FromBody] Customer customer)
        {
            var reply = _transactionService.AddCustomer(customer);
            return Ok(new ResponseMessage(reply.Result.status, reply.Result.message));
        }

        /// <summary>
        /// This API method is used to remove cutomer account from the database.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpDelete("Remove/{customerId}", Name = "RemoveCustomerAccount")]
        public IActionResult RemoveCustomerAccount(string customerId)
        {
            var reply = _transactionService.RemoveCustomerAsync(customerId);
            return Ok(new ResponseMessage(reply.Result.status, reply.Result.message));
        }

        /// <summary>
        /// This API method is used to get customers details of all customers in repository.
        /// </summary>
        /// <returns></returns>
        [HttpGet("All", Name = "GetAllCustomerDetails")]
        public IActionResult GetAllCustomers()
        {
            var reply = _transactionService.GetAllCustomers();
            if (reply.Result.status)
                return Ok(reply.Result.customers);
            else
                return BadRequest(new ResponseMessage(reply.Result.status, reply.Result.message));
        }

        /// <summary>
        /// This API method is used to get all the information of the given customer id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}", Name = "GetCustomerDetails")]
        public IActionResult GetAllCustomers(string customerId)
        {
            var reply = _transactionService.GetAccountDetailsOfCustomerAsync(customerId);
            if (reply.Result.status)
                return Ok(reply.Result.customerDetail);
            else
                return BadRequest(new ResponseMessage(reply.Result.status, reply.Result.message));
        }
    }
}
