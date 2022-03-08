using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDemo.AccountService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankDemo.AccountService.DataModel;

namespace BankDemo.AccountService.Service.Tests
{
    [TestClass]
    public class TransactionHandlerTests
    {
        ITransactionService service;
        [TestInitialize]
        public void InitializeTest()
        {
            service = new TransactionHandler(new LoggerMock(),
                new TransactionRepositoryMock(),
                new CustomerRepositoryMock());

        }
        [TestMethod]
        public void TransactionHandlerTest()
        {
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void GetTransactionsOfCustomerAsync_should_get_transaction_of_customer_Test()
        {
            var res = service.GetAccountDetailsOfCustomerAsync("TestId");
            Assert.IsTrue(res.Result.status);
        }

        [TestMethod]
        public void AddTransaction_should_add_transaction_when_amount_is_valid_Test()
        {
            var res = service.AddTransaction(new Transaction() { CustomerId = "TestId", Amount = 10 });
            Assert.IsTrue(res.Result.status);
        }

        [TestMethod]
        public void AddTransaction_should_add_transaction_when_amount_is_invalid_Test()
        {
            var res = service.AddTransaction(new Transaction() { CustomerId = "TestId", Amount = -20 });
            Assert.IsFalse(res.Result.status);
            Assert.AreEqual(res.Result.message, "Trancation amount can not be less than 0");
        }

        [TestMethod]
        public void AddTransaction_should_add_transaction_when_amount_is_more_than_account_balance_Test()
        {
            var res = service.AddTransaction(new Transaction() { CustomerId = "TestId", Amount = 20 });
            Assert.IsFalse(res.Result.status);
            Assert.AreEqual(res.Result.message, "Available balance is less than withdrawal amount.");
        }

        [TestMethod]
        public void AddTransaction_should_add_transaction_when_customer_data_invalid_Test()
        {
            var res = service.AddTransaction(new Transaction() { CustomerId = "", Amount = 20 });
            Assert.IsFalse(res.Result.status);
            Assert.AreEqual(res.Result.message, "Invalid customer id. Customer data not available.");
        }

        [TestMethod]
        public void GetAllTransactions_should_return_all_tranasactions_Test()
        {
            var res = service.GetAllTransactions();
            Assert.IsTrue(res.Result.status);
            Assert.IsTrue(res.Result.transactions.Count == 1);
        }

        [TestMethod]
        public void GetAccountDetailsOfCustomerAsync_should_return_customer_acc_detials_Test()
        {
            var res = service.GetAccountDetailsOfCustomerAsync("TestId");
            Assert.IsTrue(res.Result.status);
            Assert.IsNotNull(res.Result.customerDetail);
        }

        [TestMethod()]
        public void AddCustomer_should_add_customer_details_to_database_Test()
        {
            var res = service.AddCustomer(new Customer());
            Assert.IsTrue(res.Result.status);
        }

        [TestMethod]
        public void GetAllCustomers_should_return_customer_acc_detials_Test()
        {
            var res = service.GetAllCustomers();
            Assert.IsTrue(res.Result.status);
            Assert.IsTrue(res.Result.customers.Count == 1);
        }

    }
}