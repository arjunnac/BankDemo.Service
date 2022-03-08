using BankDemo.AccountService.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankDemo.AccountService.DataRepository
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private AppDbContext appDbContext;

        public TransactionsRepository(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        public async Task<(bool status, string message)> AddTransaction(Transaction transaction)
        {
            if (transaction.Validate())
            {
                appDbContext.Transactions.Add(transaction);
                return (true, "Tranaction is successfull.");
            }
            else
            {
                return (false, "Transaction is not valid.");
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionOfCustomer(string customerId)
        {
            if (!string.IsNullOrEmpty(customerId))
            {
                return appDbContext.Transactions.Where(x => x.CustomerId.Equals(customerId)).ToList<Transaction>();
            }
            return null;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return appDbContext.Transactions.ToList<Transaction>();
        }
    }
}
