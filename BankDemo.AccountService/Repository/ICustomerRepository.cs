using BankDemo.AccountService.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDemo.AccountService.DataRepository
{
    public interface ICustomerRepository
    {
        Task<(bool status, string message)> AddCustomerAsync(Customer customer);
        Task<(bool status, string message)> UpdateCustomerBalanceAsync(string id, double balance);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetAccountDetailsCustomerOfAsync(string id);
        Task<(bool status, string message)> RemoveCustomerAsync(string id);
    }
}
