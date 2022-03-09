using BankDemo.AccountService.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankDemo.AccountService.DataRepository
{
    /// <summary>
    /// Customer db interaction.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext appDbContext;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbContext"></param>
        public CustomerRepository(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        public async Task<(bool status, string message)> AddCustomerAsync(Customer customer)
        {
            string validationMessage = CheckCustomerData(customer);
            bool status = string.IsNullOrEmpty(validationMessage);
            if (status)
            {
                if (CheckCustomerExistInDb(customer.Id))
                {
                    status = false;
                    validationMessage = $"CustomerId {customer.Id} is not available.";
                }
                else
                {
                    appDbContext.Customers.Add(customer);
                    appDbContext.Transactions.Add(new Transaction(customer.Id, customer.Balance));
                    validationMessage = $"Customer {customer.Id} created successfully.";
                }
            }

            return (status, validationMessage);
        }

        public async Task<Customer> GetAccountDetailsCustomerOfAsync(string id)
        {
            var customer = appDbContext.Customers.First(x => x.Id == id);
            customer.Transactions.Clear();
            var transactions = appDbContext.Transactions.Where(x => x.CustomerId == id).ToList();
            transactions.ForEach(t => customer.Transactions.Add(t));
            return await Task.FromResult(customer);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return appDbContext.Customers.ToList<Customer>();
        }

        public async Task<(bool status, string message)> RemoveCustomerAsync(string id)
        {
            try
            {
                if (CheckCustomerExistInDb(id))
                {
                    var customer = appDbContext.Customers.First(x => x.Id == id);
                    appDbContext.Customers.Remove(customer);
                    appDbContext.Transactions.RemoveAll(x => x.CustomerId == customer.Id);
                    return (true, $"Customer account removed successfully.");
                }
                return (false, $"Customer id {id} does not exist.");
            }
            catch (Exception ex)
            {
                return (false, $"exception to remove customer. {ex.Message}");
            }
        }

        public async Task<(bool status, string message)> UpdateCustomerBalanceAsync(string id, double balance)
        {
            try
            {
                appDbContext.Customers.FirstOrDefault(x => x.Id == id).Balance = balance;
                return (true, "");
            }
            catch (System.Exception)
            {
                return (false, "Error in updating customer account balance");
            }


        }

        private bool CheckCustomerExistInDb(string customerId)
        {
            return appDbContext.Customers.FirstOrDefault(cust => cust.Id == customerId) != null;
        }

        private string CheckCustomerData(Customer customer)
        {
            return customer.Validate();
        }

    }
}
