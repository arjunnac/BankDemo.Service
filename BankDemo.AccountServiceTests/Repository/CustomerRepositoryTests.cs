using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDemo.AccountService.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDemo.AccountService.DataModel;

namespace BankDemo.AccountService.DataRepository.Tests
{
    [TestClass()]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public void Check_Customer_Invalid_Data_On_Addition_To_Repository_Test()
        {
            ICustomerRepository repo = new CustomerRepository(new AppDbContext());
            var customer = new Customer() { FirstName = "Fname", LastName = "Lname" };
            var reply = repo.AddCustomerAsync(customer);
            Assert.IsFalse(reply.Result.status);

            customer.Id = "Test1"; customer.Balance = 1;
            reply = repo.AddCustomerAsync(customer);
            Assert.IsFalse(reply.Result.status);


            customer.Id = "Test1234"; customer.Balance = -1;
            reply = repo.AddCustomerAsync(customer);
            Assert.IsFalse(reply.Result.status);

            customer.Id = "Test1234"; customer.Balance = 100;
            reply = repo.AddCustomerAsync(customer);
            Assert.IsTrue(reply.Result.status);
        }

        [TestMethod]
        public void Check_Customer_Data_Exist_In_Repository_on_Addition_Test()
        {
            ICustomerRepository repo = new CustomerRepository(new AppDbContext());
            var replay = repo.AddCustomerAsync(new Customer());
            Assert.IsFalse(replay.Result.status);

            replay = repo.AddCustomerAsync(new Customer { Balance = 100, Id = "Test_Id1", FirstName = "Fname", LastName = "Lname" });
            Assert.IsTrue(replay.Result.status);

            replay = repo.AddCustomerAsync(new Customer { Balance = 200, Id = "Test_Id2", FirstName = "Fname", LastName = "Lname" });
            Assert.IsTrue(replay.Result.status);
        }

        [TestMethod]
        public void Check_Customer_Data_Exist_In_Repository_and_do_not_Add_if_exist_Test()
        {
            ICustomerRepository repo = new CustomerRepository(new AppDbContext());
            var replay = repo.AddCustomerAsync(new Customer());
            Assert.IsFalse(replay.Result.status);

            replay = repo.AddCustomerAsync(new Customer { Balance = 100, Id = "Test_Id1", FirstName = "Fname", LastName = "Lname" });
            Assert.IsTrue(replay.Result.status);

            replay = repo.AddCustomerAsync(new Customer { Balance = 200, Id = "Test_Id1", FirstName = "Fname", LastName = "Lname" });
            Assert.IsFalse(replay.Result.status);
            Assert.AreEqual(replay.Result.message, "Customer Test_Id1 can not be created.");
        }

        [TestMethod]
        public void Get_All_Customer_Data_Should_Return_No_data()
        {
            ICustomerRepository repo = new CustomerRepository(new AppDbContext());
            var replay = repo.AddCustomerAsync(new Customer());
            Assert.IsFalse(replay.Result.status);

            var customers = repo.GetAllCustomersAsync();
            Assert.IsTrue(customers.Result.Count == 1);
        }

        [TestMethod]
        public void Get_All_Customer_Data_Exist_In_Repository()
        {
            ICustomerRepository repo = new CustomerRepository(new AppDbContext());
            var replay = repo.AddCustomerAsync(new Customer());
            Assert.IsFalse(replay.Result.status);

            replay = repo.AddCustomerAsync(new Customer { Balance = 100, Id = "Test_Id1", FirstName = "Fname", LastName = "Lname" });
            Assert.IsTrue(replay.Result.status);

            replay = repo.AddCustomerAsync(new Customer { Balance = 200, Id = "Test_Id2", FirstName = "Fname", LastName = "Lname" });
            Assert.IsTrue(replay.Result.status);

            var customers = repo.GetAllCustomersAsync();
            Assert.IsTrue(customers.Result.Count == 3);
        }
    }
}