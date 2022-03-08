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
        [Key]
        public string TransactionId { get; }
        public string CustomerId { get; set; }
        public DateTime Date { get; }
        public int TransactionAction { get; set; }
        public double Amount { get; set; } = 0;
        //public double PrevBalance { get; set; } = 0;
        //public double CurrentBalance { get; set; } = 0;

        public Transaction()
        {
            TransactionId = Guid.NewGuid().ToString();
            Date = DateTime.UtcNow;
        }

        public Transaction(string custmerId, double amount) : this()
        {
            this.CustomerId = custmerId;
            this.Amount = amount;
        }
        // helpers
        public TransactionType GetTransactionType()
        {
            return (TransactionType)TransactionAction;
        }


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
