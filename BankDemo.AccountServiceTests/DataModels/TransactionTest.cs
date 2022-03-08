using BankDemo.AccountService.DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankDemo.AccountService.Test
{
    [TestClass]
    public class BankTransactionTest
    {

        [TestMethod]
        public void Transaction_Prop_Tests()
        {
            var transaction = new Transaction() { CustomerId = "TestId", Amount = 100 };
            Assert.AreEqual(transaction.CustomerId, "TestId");
            Assert.AreEqual(transaction.Amount, 100);
            Assert.IsFalse(string.IsNullOrEmpty(transaction.TransactionId));
        }

        [TestMethod]
        public void Tranaction_Validate_Amount_Tests()
        {
            var transaction = new Transaction() { CustomerId = "Test12", Amount = -100 };
            Assert.AreEqual(transaction.CustomerId, "Test12");
            Assert.IsFalse(transaction.Validate());

            transaction = new Transaction() { CustomerId = "Test12", Amount = 100 };
            Assert.AreEqual(transaction.CustomerId, "Test12");
            Assert.IsTrue(transaction.Validate());
        }

        [TestMethod]
        public void Tranaction_Validate_CustomerId_Tests()
        {
            var transaction = new Transaction() { CustomerId = "", Amount = 100 };
            Assert.AreEqual(transaction.CustomerId, "");
            Assert.IsFalse(transaction.Validate());

            transaction = new Transaction() { CustomerId = "TestId", Amount = 100 };
            Assert.AreEqual(transaction.CustomerId, "TestId");
            Assert.IsTrue(transaction.Validate());
        }

        [TestMethod]
        public void Tranaction_Validate_TransactionType_Tests()
        {
            var transaction = new Transaction() { CustomerId = "TestId", Amount = 100, TransactionAction = 0 };
            Assert.AreEqual(transaction.CustomerId, "TestId");
            Assert.IsTrue(transaction.Validate());
            Assert.AreEqual(transaction.GetTransactionType(), Common.TransactionType.Debit);
            transaction = new Transaction() { CustomerId = "TestId", Amount = 100, TransactionAction = 1 };
            Assert.AreEqual(transaction.CustomerId, "TestId");
            Assert.IsTrue(transaction.Validate());
            Assert.AreEqual(transaction.GetTransactionType(), Common.TransactionType.Credit);

            transaction = new Transaction() { CustomerId = "TestId", Amount = 100, TransactionAction = 3 };
            Assert.AreEqual(transaction.CustomerId, "TestId");
            Assert.IsTrue(transaction.Validate());
            Assert.AreNotEqual(transaction.GetTransactionType(), Common.TransactionType.Credit);

            transaction = new Transaction() { CustomerId = "TestId", Amount = 100, TransactionAction = -1 };
            Assert.AreEqual(transaction.CustomerId, "TestId");
            Assert.IsTrue(transaction.Validate());
            Assert.AreNotEqual(transaction.GetTransactionType(), Common.TransactionType.Debit);
        }
    }
}
