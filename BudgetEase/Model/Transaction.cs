namespace BudgetEasee.Models
{
    public enum TransactionType
    {
        Debit,
        Credit
    }

    public class Transaction
    {
        public int Id { get; set; } // Primary Key
        public string Source { get; set; } // Source of the transaction
        public decimal Amount { get; set; } // Amount of the transaction
        public DateTime Date { get; set; } // Date of the transaction
        public TransactionType TransactionType { get; set; } // Debit or Credit
        public string Tag { get; set; } // Tag for categorizing the transaction
        public string Remarks { get; set; } // Additional notes or remarks
    }
}
