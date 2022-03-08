using BankDemo.AccountService.DataModel;
using System.Collections.Generic;

namespace BankDemo.AccountService.DataRepository
{
    public class AppDbContext /*: Microsoft.EntityFrameworkCore.DbContext*/
    {
        //public AppDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options)
        //   : base(options)
        //{
        //}
        public AppDbContext()
        {
            this.Customers = new List<Customer>() { new Customer() { Id = "Cust123", FirstName = "Amit", LastName = "Chavan", Balance = 1000 } };
            this.Transactions = new List<Transaction>();
            this.Transactions.Add(new Transaction() { CustomerId = "Cust123", Amount = 1000 });
        }

        //public DbSet<Customer> Users { get; set; }
        public List<Customer> Customers { get; }
        //public DbSet<Transaction> Transactions { get; set; }#
        public List<Transaction> Transactions { get; }
    }
}
