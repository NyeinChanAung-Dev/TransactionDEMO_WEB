using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionDEMO_WEB.Models;

namespace TransactionDEMO_WEB.Helper
{
    public static class TransactionHelper
    {
        internal static List<string> ValidateTransactions(List<Transaction> transactions)
        {
            var errors = new List<string>();

            foreach (var transaction in transactions)
            {
                if (transaction.Amount == null)
                    errors.Add($"Amount is null for transaction {transaction.TransactionId}");

                if (string.IsNullOrEmpty(transaction.CurrencyCode))
                    errors.Add($"CurrencyCode is null for transaction {transaction.TransactionId}");

                if (string.IsNullOrEmpty(transaction.Status))
                    errors.Add($"Status is null for transaction {transaction.TransactionId}");

                if (transaction.TransactionDate == new DateTime())
                    errors.Add($"TransactionDate is invalid for transaction {transaction.TransactionId}");
                if (string.IsNullOrEmpty(transaction.TransactionId))
                    errors.Add($"TransactionId is empty for transaction {transaction.TransactionId}");
                if (transaction.TransactionId.Trim().Length > 50)
                    errors.Add($"TransactionId is too long for transaction {transaction.TransactionId}");
            }

            return errors;
        }
    }

}
