using BankDemo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankDemo.AccountService.DataModel
{
    public class Customer
    {

        /// Customer ID
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// First name of customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date of customer account creation
        /// </summary>
        public DateTime CreationDateTime { get;  }

        /// <summary>
        /// Customer account balance.
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// Transactions perfomed in the customer account.
        /// </summary>
        public IList<Transaction> Transactions { get;}

        /// <summary>
        /// ctor
        /// </summary>
        public Customer()
        {
            CreationDateTime = DateTime.UtcNow;
            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Customer data validation
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            if (string.IsNullOrEmpty(Id))
                return "Customer ID is not valid.";
            if (string.IsNullOrEmpty(FirstName))
                return "First name is not valid.";
            if (string.IsNullOrEmpty(LastName))
                return "Last name is not valid.";
            if (this.Id.Length < 6 || this.Id.Length > 8)
                return "Customer ID should be of 6 to 8 chars.";
            if (this.FirstName.HasStringNumbers())
                return "Invalid char in First name.";
            if (this.LastName.HasStringNumbers())
                return "Invalid char in Last name.";
            if (this.Balance <= 0)
                return "Amount entered is not valid.";
            return string.Empty;
        }
    }
}
