using Microsoft.EntityFrameworkCore;
using BudgetEasee.Data;
using BudgetEasee.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetEasee.Services
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new transaction
        public async Task<bool> AddTransactionAsync(string source, decimal amount, TransactionType transactionType, string tag, string remarks)
        {
            var transaction = new Transaction
            {
                Source = source,
                Amount = amount,
                Date = System.DateTime.Now, // Set the current date for the transaction
                TransactionType = transactionType,
                Tag = tag,
                Remarks = remarks
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all transactions
        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<List<string>> GetAvailableTagsAsync()
        {
            return await _context.Transactions
                                 .Select(t => t.Tag)
                                 .Distinct()
                                 .ToListAsync();
        }


        // Get transactions by type (Debit/Credit)
        public async Task<List<Transaction>> GetTransactionsByTypeAsync(TransactionType transactionType)
        {
            return await _context.Transactions
                                 .Where(t => t.TransactionType == transactionType)
                                 .ToListAsync();
        }

        // Get transactions for a specific date
        public async Task<List<Transaction>> GetTransactionsByDateAsync(System.DateTime date)
        {
            return await _context.Transactions
                                 .Where(t => t.Date.Date == date.Date)
                                 .ToListAsync();
        }

        // Get transactions by tag (optional filtering)
        public async Task<List<Transaction>> GetTransactionsByTagAsync(string tag)
        {
            return await _context.Transactions
                                 .Where(t => t.Tag.Contains(tag)) // Filter by partial tag match
                                 .ToListAsync();
        }

        // Delete a transaction by ID
        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction == null)
            {
                return false; // Transaction not found
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
