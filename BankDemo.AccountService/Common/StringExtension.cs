using System.Linq;

namespace BankDemo.Common
{
    public static class ExtStrings
    {
        public static bool HasStringNumbers(this string inputStr)
        {
            return inputStr?.Any(char.IsDigit) ?? false;
        }
    }

    // transaction type enum
    public enum TransactionType
    {
        Credit = 1,
        Debit = 0,
    }
}
