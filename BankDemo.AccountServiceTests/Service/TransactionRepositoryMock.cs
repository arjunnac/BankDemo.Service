using BankDemo.AccountService.DataModel;
using BankDemo.AccountService.DataRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDemo.AccountService.Service.Tests
{
    internal class TransactionRepositoryMock : ITransactionsRepository
    {
        public async Task<(bool status, string message)> AddTransaction(Transaction transaction) => (true, "");

        public async Task<IEnumerable<Transaction>> GetAllTransactionOfCustomer(string customerId) => new List<Transaction>() { new Transaction() };

        public async Task<List<Transaction>> GetAllTransactions() => new List<Transaction>() { new Transaction() };
    }
}