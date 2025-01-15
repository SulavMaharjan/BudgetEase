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
        public string Source { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Tag { get; set; }
        public string Remarks { get; set; }
    }
}
