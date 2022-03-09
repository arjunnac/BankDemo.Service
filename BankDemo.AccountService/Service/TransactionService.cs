using BankDemo.AccountService.DataModel;
using BankDemo.AccountService.DataRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDemo.AccountService.Service
{
    /// <summary>
    /// Handles transaction of the repositories
    /// </summary>
    public class TransactionHandler : ITransactionService
    {
        ITransactionsRepository _transactionsRepository;
        ICustomerRepository _customerRepository;
        ILogger<TransactionHandler> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="transactionsRepository"></param>
        /// <param name="customerRepository"></param>
        public TransactionHandler(ILogger<TransactionHandler> logger,
                                  ITransactionsRepository transactionsRepository,
                                  ICustomerRepository customerRepository)
        {
            _logger = logger;
            _transactionsRepository = transactionsRepository;
            _customerRepository = customerRepository;
        }



        #region Transaction Details
        public async Task<(bool status, string message, IEnumerable<Transaction> transactions)> GetTransactionsOfCustomerAsync(string customerID)
        {
            try
            {
                var resultTask = await _transactionsRepository.GetAllTransactionOfCustomer(customerID);
                return (true, string.Empty, resultTask);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetTransactionsOfUserAsync - exception" + ex.Message);
                return (false, $"Error to get all transactions of the selected customer of {customerID}", null);
            }
        }

        public async Task<(bool status, string message)> AddTransaction(Transaction transaction)
        {
            try
            {
                // Some amount need to add it should be positive value
                if (transaction.Amount <= 0)
                {
                    // return error
                    return new(false, "Trancation amount can not be less than 0");
                }

                // get customer details.
                var customer = await _customerRepository.GetAccountDetailsCustomerOfAsync(transaction.CustomerId);
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
                    return new(false, "Available balance is less than withdrawal amount.");
                }
                var updateResult = await _customerRepository.UpdateCustomerBalanceAsync(customer.Id, availableBalance);
                return await _transactionsRepository.AddTransaction(transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError("AddTransaction - exception" + ex.Message);
                return new(false, $"Error to add transaction for customer {transaction.CustomerId}");
            }
        }

        public async Task<(bool status, string message, List<Transaction> transactions)> GetAllTransactions()
        {
            try
            {
                var resultTask = await _transactionsRepository.GetAllTransactions();
                return (true, string.Empty, resultTask);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetAllTransactions - exception" + ex.Message);
                return (false, $"Error to get all transactions..", null);
            }
        }

        #endregion


        #region Customer Details

        public async Task<(bool status, string message, Customer customerDetail)> GetAccountDetailsOfCustomerAsync(string id)
        {
            try
            {
                var resultTask = await _customerRepository.GetAccountDetailsCustomerOfAsync(id);
                return (true, string.Empty, resultTask);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetAccountDetailsOfCustomerAsync - exception" + ex.Message);
                return (false, $"Error to get account details of selected customer  {id}", null);
            }
        }

        public async Task<(bool status, string message)> AddCustomer(Customer customer)
        {
            try
            {
                return await _customerRepository.AddCustomerAsync(customer);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("AddCustomer - exception" + ex.Message);
                return (false, $"Error to add account of selected customer  {customer.Id}");
            }
        }

        public async Task<(bool status, string message, List<Customer> customers)> GetAllCustomers()
        {
            try
            {
                var resultTask = await _customerRepository.GetAllCustomersAsync();
                return (true, "", resultTask);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetAccountDetailsOfCustomerAsync - exception" + ex.Message);
                return (false, $"Error to all the customer details", null);
            }
        }

        public async Task<(bool status, string message)> RemoveCustomerAsync(string customerId)
        {
            try
            {
                return await _customerRepository.RemoveCustomerAsync(customerId);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("AddCustomer - exception" + ex.Message);
                return (false, $"Error to add account of selected customer  {customerId}");
            }
        }
        #endregion
    }


}
