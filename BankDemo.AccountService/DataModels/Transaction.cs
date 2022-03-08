using BankDemo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDemo.AccountService.DataModel
{
    public class Transaction
    {
        /// <summary>
        /// Transaction unique id 
        /// </summary>
        [Key]
        public string TransactionId { get; }

        /// <summary>
        /// Customer id performed transaction.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Transaction date time.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Transaction action - 0 indicating "Debit" and 1 indicating "Credit". 
        /// </summary>
        public int TransactionAction { get; set; }

        /// <summary>
        /// Transaction amount.
        /// </summary>
        public double Amount { get; set; } = 0;
        
        /// <summary>
        /// ctor
        /// </summary>
        public Transaction()
        {
            TransactionId = Guid.NewGuid().ToString();
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="custmerId"></param>
        /// <param name="amount"></param>
        public Transaction(string custmerId, double amount) : this()
        {
            this.CustomerId = custmerId;
            this.Amount = amount;
        }
        
        /// <summary>
        /// return transaction type
        /// </summary>
        /// <returns></returns>
        public TransactionType GetTransactionType()
        {
            return (TransactionType)TransactionAction;
        }

        /// <summary>
        /// Transaction data validation.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            if (string.IsNullOrEmpty(CustomerId))
                return false;
            if (this.Amount <= 0)
                return false;
            if (!Guid.TryParse(this.TransactionId, out Guid res))
                return false;
            return true;
        }
    }
}
