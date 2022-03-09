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
        Task<(bool status, string message, IEnumerable<Transaction> transactions)> GetTransactionsOfCustomerAsync(string customerID);
        Task<(bool status, string message)> AddTransaction(Transaction transaction);
        Task<(bool status, string message, List<Transaction> transactions)> GetAllTransactions();
        #endregion

        #region Customer Details
        Task<(bool status, string message, Customer customerDetail)> GetAccountDetailsOfCustomerAsync(string customerId);
        Task<(bool status, string message)> AddCustomer(Customer customer);
        Task<(bool status, string message, List<Customer> customers)> GetAllCustomers();
        Task<(bool status, string message)> RemoveCustomerAsync(string customerId);
        #endregion
    }
}
