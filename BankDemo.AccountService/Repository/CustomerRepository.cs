using BankDemo.AccountService.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankDemo.AccountService.DataRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext appDbContext;

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
                if (CheckAndAddCustomerExistInDb(customer))
                {
                    status = false;
                    validationMessage = $"Customer {customer.Id} can not be created.";
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

        public async Task<Customer> GetAccountUserOfAsync(string id)
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

        private bool CheckAndAddCustomerExistInDb(Customer customer)
        {
            return appDbContext.Customers.FirstOrDefault(id => id.Id.Equals(customer.Id)) != null;
        }

        private string CheckCustomerData(Customer customer)
        {
            return customer.Validate();
        }

    }
}
