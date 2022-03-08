using BankDemo.AccountService.DataModel;
using BankDemo.AccountService.DataRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDemo.AccountService.Service
{
    public class TransactionHandler : ITransactionService
    {
        ITransactionsRepository _transactionsRepository;
        ICustomerRepository _customerRepository;

        public TransactionHandler(ITransactionsRepository transactionsRepository,
                                  ICustomerRepository customerRepository)
        {
            _transactionsRepository = transactionsRepository;
            _customerRepository = customerRepository;
        }



        #region Transaction Details
        public async Task<IEnumerable<Transaction>> GetTransactionsOfUserAsync(string customerID)
        {
            return await _transactionsRepository.GetAllTransactionOfCustomer(customerID);
        }

        public async Task<(bool status, string message)> AddTransaction(Transaction transaction)
        {
            // Some amount need to add it should be positive value
            if (transaction.Amount <= 0)
            {
                // return error
                return new(false, "Trancation amount can not be less than 0");
            }

            // get customer details.
            var customer = await _customerRepository.GetAccountUserOfAsync(transaction.CustomerId);
            if (customer == null)
            {
                return new(false, "Invalid customer id. Customer data not available.");
            }

            // check for balance in case of the debit transaction.

            double customerBalance = customer.Balance;
            double availableBalance = customerBalance + (transaction.GetTransactionType() == Common.TransactionType.Credit ? transaction.Amount : -transaction.Amount);
            if (availableBalance < 0)
            {
                // return error
                return new(false, "Invalid customer id. Customer data not available.");
            }
            var updateResult = await _customerRepository.UpdateCustomerBalanceAsync(customer.Id, availableBalance);
            return await _transactionsRepository.AddTransaction(transaction);
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await _transactionsRepository.GetAllTransactions();
        }

        #endregion


        #region Customer Details

        // get single user
        public async Task<Customer> GetAccountDetailsOfCustomerAsync(string id)
        {
            return await _customerRepository.GetAccountUserOfAsync(id);
        }

        public async Task<(bool status, string message)> AddCustomer(Customer customer)
        {
            return await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }
        #endregion
    }


}
