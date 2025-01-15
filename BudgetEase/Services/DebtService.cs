using Microsoft.EntityFrameworkCore;
using BudgetEasee.Data;
using BudgetEasee.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetEasee.Services
{
    public class DebtService
    {
        private readonly ApplicationDbContext _context;
        private List<Debt> _inMemoryDebts = null;  // Store debts in memory for temporary use

        public DebtService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new debt
        public async Task<bool> AddDebtAsync(string source, decimal amount, DateTime debtDueDate)
        {
            try
            {
                var debt = new Debt
                {
                    Source = source,
                    Amount = amount,
                    DebtDueDate = debtDueDate,
                    Status = "Pending"  // Default status is set to Pending
                };

                _context.Debts.Add(debt);
                await _context.SaveChangesAsync();

                // After adding to the database, refresh in-memory debt list
                _inMemoryDebts = null;  // Invalidate the in-memory cache to reload fresh data from DB
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding debt: {ex.Message}");
                return false;
            }
        }

        // Get all debts
        public async Task<List<Debt>> GetDebtsAsync()
        {
            if (_inMemoryDebts == null)  // If not loaded, load debts from DB
            {
                _inMemoryDebts = await _context.Debts.ToListAsync();
            }
            return _inMemoryDebts;
        }

        // Delete a debt by Id
        public async Task<bool> DeleteDebtAsync(int debtId)
        {
            try
            {
                var debt = await _context.Debts.FirstOrDefaultAsync(d => d.Id == debtId);

                if (debt == null)
                {
                    return false; // Debt not found
                }

                _context.Debts.Remove(debt);
                await _context.SaveChangesAsync();

                // After deleting, invalidate the in-memory cache and reload data
                _inMemoryDebts = null;
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting debt: {ex.Message}");
                return false;
            }
        }

        // Update the status of a debt
        public async Task<bool> UpdateDebtStatusAsync(int debtId, string status)
        {
            try
            {
                var debt = await _context.Debts.FirstOrDefaultAsync(d => d.Id == debtId);
                if (debt == null)
                {
                    return false; // Debt not found
                }

                debt.Status = status;
                _context.Debts.Update(debt);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating debt status: {ex.Message}");
                return false;
            }
        }
    }
}
