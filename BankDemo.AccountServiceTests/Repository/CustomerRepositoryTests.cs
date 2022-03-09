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
            Assert.AreEqual(replay.Result.message, "CustomerId Test_Id1 is not available.");
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

        [TestMethod]
        public void RemoveCustomerAsync_should_remove_account_from_repository()
        {
            ICustomerRepository repo = new CustomerRepository(new AppDbContext());
            var customer = new Customer() { FirstName = "Fname", LastName = "Lname" };
            customer.Id = "TestIdX"; customer.Balance = 15;
            repo.AddCustomerAsync(customer);

            customer = new Customer() { FirstName = "FnameTwo", LastName = "LnameTwo" };
            customer.Id = "TestIdY"; customer.Balance = 30;
            repo.AddCustomerAsync(customer);

            var reply = repo.RemoveCustomerAsync("TestIdX");
            Assert.IsTrue(reply.Result.status);
            Assert.AreEqual(reply.Result.message, "Customer account removed successfully.");
        }

        [TestMethod]
        public void RemoveCustomerAsync_should_not_remove_acount_from_repository()
        {
            ICustomerRepository repo = new CustomerRepository(new AppDbContext());
            var customer = new Customer() { FirstName = "Fname", LastName = "Lname" };
            customer.Id = "TestIdX"; customer.Balance = 15;
            repo.AddCustomerAsync(customer);

            var reply = repo.RemoveCustomerAsync("TestIdY");
            Assert.IsFalse(reply.Result.status);
            Assert.AreEqual(reply.Result.message, "Customer id TestIdY does not exist.");
        }


    }
}