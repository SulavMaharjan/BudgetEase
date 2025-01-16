using Microsoft.EntityFrameworkCore;
using BudgetEasee.Data;
using BudgetEasee.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BudgetEasee.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string username, string password, string preferredCurrency)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (existingUser != null)
            {
                return false; // User already exists
            }

            var passwordHash = HashPassword(password);
            var user = new User { Username = username, PasswordHash = passwordHash, PreferredCurrency = preferredCurrency };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> LoginUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user; // Return the user object upon successful login
            }

            return null; // Login failed
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hashedPassword = HashPassword(password);
            return storedHash == hashedPassword;
        }
    }
}
