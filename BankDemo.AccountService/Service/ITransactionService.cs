using BankDemo.AccountService.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankDemo.AccountService.Service
{
    public interface ITransactionService
    {
        #region Transaction Details
        Task<IEnumerable<Transaction>> GetTransactionsOfUserAsync(string customerID);
        Task<(bool status, string message)> AddTransaction(Transaction transaction);
        Task<List<Transaction>> GetAllTransactions();
        #endregion

        #region Customer Details
        Task<Customer> GetAccountDetailsOfCustomerAsync(string customerId);
        Task<(bool status, string message)> AddCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        #endregion
    }
}
