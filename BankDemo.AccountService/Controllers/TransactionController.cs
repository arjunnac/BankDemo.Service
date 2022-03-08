using BankDemo.AccountService.DataModel;
using BankDemo.AccountService.Service;
using BankDemo.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BankDemo.AccountService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Add transaction is used to do transacation for given customer
        /// To Credit amount select "TransactionAction" as "1"
        /// To Debit amount select "TransactionAction" as "0"
        /// Any other value will be ignored
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPut(Name = "AddTransactions")]
        public ActionResult<ResponseMessage> Put([FromBody] Transaction transaction)
        {
            var reply = _transactionService.AddTransaction(transaction);
            return new ResponseMessage(reply.Result.status, reply.Result.message);
        }

        [HttpGet("customer/{id}", Name = "GetAllTransactionsOfCustomer")]
        public IActionResult GetAllTransactions(string id)
        {
            var trans = _transactionService.GetTransactionsOfUserAsync(id);
            if (trans.Result != null)
                return Ok(trans.Result);
            else
                return NotFound();
        }

        [HttpGet("All", Name = "GetAllTransactions")]
        public ActionResult<List<Transaction>> Get()
        {
            var trans = _transactionService.GetAllTransactions();
            return trans.Result.ToList<Transaction>();
        }
    }
}
