using BankDemo.AccountService.DataModel;
using BankDemo.AccountService.DataRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BankDemo.AccountService.Test
{
    [TestClass]
    public class TransactionsRepositoryTest
    {
        TransactionsRepository repo;
        [TestInitialize]
        public void InitializeTest()
        {
            repo = new TransactionsRepository(new AppDbContext());
        }

        [TestMethod]
        public void Check_transaction_has_invalid_data_on_addition_to_repository_test()
        {
            //If custmoreid is not valid.
            var transaction = new Transaction("", 500);
            var result = repo.AddTransaction(transaction);
            Assert.IsFalse(result.Result.status);
            Assert.AreEqual(result.Result.message, "Transaction is not valid.");

            //If amount is not valid.
            transaction = new Transaction("CustId", 0);
            result = repo.AddTransaction(transaction);
            Assert.IsFalse(result.Result.status);
            Assert.AreEqual(result.Result.message, "Transaction is not valid.");
        }

        [TestMethod]
        public void Check_transaction_is_valid_and_unique_on_addition_to_repository_test()
        {
            //If transaction is  valid.
            var transaction = new Transaction("CustId", 500);
            var result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            transaction = new Transaction("CustId", 50);
            result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            transaction = new Transaction("CustId", 100);
            result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            var transactionsOfCustomer = repo.GetAllTransactionOfCustomer("CustId");
            Assert.AreEqual(transactionsOfCustomer.Result.ToList()?.Count, 3);
        }

        [TestMethod]
        public void Check_transaction_is_valid_and_customerId_empty_then_should_not_return_transactions()
        {
            //If custmoreid is  valid.
            var transaction = new Transaction("CustId", 500);
            var result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            // customer id is not given
            var transactionsOfCustomer = repo.GetAllTransactionOfCustomer("");
            Assert.IsNull(transactionsOfCustomer.Result);
        }

        [TestMethod]
        public void Check_transaction_is_valid_for_customer_should_return_transactions_of_customer()
        {
            //custmoreid  CustId1 is  valid.
            var transaction = new Transaction("CustId1", 500);
            var result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            transaction = new Transaction("CustId1", 50);
            result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            transaction = new Transaction() { CustomerId = "CustId1", Amount = 80 };
            result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");


            //custmoreid  CustId2 is  valid.
            transaction = new Transaction("CustId2", 75);
            result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            transaction = new Transaction("CustId2", 1000);
            result = repo.AddTransaction(transaction);
            Assert.IsTrue(result.Result.status);
            Assert.AreEqual(result.Result.message, "Tranaction is successfull.");

            // customer id 1 is given
            var transactionsOfCustomer = repo.GetAllTransactionOfCustomer("CustId1");
            Assert.AreEqual(transactionsOfCustomer.Result.ToList().Count, 3);

            // customer id 2 is given
            transactionsOfCustomer = repo.GetAllTransactionOfCustomer("CustId2");
            Assert.AreEqual(transactionsOfCustomer.Result.ToList().Count, 2);
        }
    }
}
