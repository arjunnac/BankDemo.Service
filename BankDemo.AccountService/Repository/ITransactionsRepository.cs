using BankDemo.AccountService.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDemo.AccountService.DataRepository
{
    public interface ITransactionsRepository
    {
        Task<(bool status, string message)> AddTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllTransactionOfCustomer(string customerId);
        Task<List<Transaction>> GetAllTransactions();
    }
}
