using BankDemo.AccountService.DataModel;
using BankDemo.AccountService.DataRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDemo.AccountService.Service.Tests
{
    internal class CustomerRepositoryMock : ICustomerRepository
    {
        public async Task<(bool status, string message)> AddCustomerAsync(Customer customer)
        {
            return (true, "");
        }

        public async Task<Customer> GetAccountDetailsCustomerOfAsync(string id)
        {
            if (id == "TestId")
                return new Customer() { Balance = 10 };
            else
                return null;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return new List<Customer>() { new Customer() };
        }

        public async Task<(bool status, string message)> RemoveCustomerAsync(string id)
        {
            return id == "TestId" ? (true, "") : (false, "");
        }

        public async Task<(bool status, string message)> UpdateCustomerBalanceAsync(string id, double balance)
        {
            return (true, "");
        }
    }
}