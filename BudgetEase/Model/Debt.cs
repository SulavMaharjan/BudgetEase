using System;

namespace BudgetEasee.Models
{
    public class Debt
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
        public DateTime DebtDueDate { get; set; }
        public string Status { get; set; } = "Pending"; // Default status is "Pending"
    }
}
