using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDemo.AccountService.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDemo.AccountService.DataModel.Tests
{
    [TestClass()]
    public class CustomerTests
    {
        [TestMethod]
        public void Customer_Properties_Test()
        {
            var customer = new Customer { Id = "TestId" };
            Assert.AreEqual(customer.Id, "TestId");
            customer.Balance = 100;
            Assert.AreEqual(customer.Balance, 100);
        }

        [TestMethod]
        public void Customer_Validation_Id_Tests()
        {
            // empty customer id
            var customer = new Customer { Balance = 1, FirstName = "Fname", LastName = "Lname" };
            string response = customer.Validate();
            Assert.AreEqual(response, "Customer ID is not valid.");

            // valid customer id with more than 6 char less than 8
            customer.Id = "TestId1";
            response = customer.Validate();
            Assert.AreEqual(response, "");

            // invalid customer id with 6 char
            customer.Id = "Test12";
            response = customer.Validate();
            Assert.AreEqual(response, "");

            // invalid customer id with 6 char
            customer.Id = "Test";
            response = customer.Validate();
            Assert.AreEqual(response, "Customer ID should be of 6 to 8 chars.");

            // invalid customer id with 8 char
            customer.Id = "TestId12";
            response = customer.Validate();
            Assert.AreEqual(response, "");

            // invalid customer id with more than 8 char
            customer.Id = "TestId123";
            response = customer.Validate();
            Assert.AreEqual(response, "Customer ID should be of 6 to 8 chars.");
        }

        [TestMethod]
        public void Customer_Validation_Amount_Tests()
        {
            // 0 amount
            var customer = new Customer { Id = "TestId12", FirstName = "Fname", LastName = "Lname" };
            string response = customer.Validate();
            Assert.AreEqual(response, "Amount entered is not valid.");

            //-ve amount
            customer.Balance = -10;
            response = customer.Validate();
            Assert.AreEqual(response, "Amount entered is not valid.");

            //+ve case
            customer.Balance = 1000;
            response = customer.Validate();
            Assert.AreEqual(response, "");
        }


        [TestMethod]
        public void Customer_Validation_Name_Tests()
        {
            // 0 amount
            var customer = new Customer { Id = "TestId12", Balance = 100, LastName = "Lname" };
            customer.FirstName = "Test12";
            string response = customer.Validate();
            Assert.AreEqual(response, "Invalid char in First name.");

            //-ve amount
            customer.FirstName = "Fname";
            customer.LastName = "Lname123";
            response = customer.Validate();
            Assert.AreEqual(response, "Invalid char in Last name.");
        }
    }
}