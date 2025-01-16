using System.ComponentModel.DataAnnotations;

namespace BudgetEasee.Models
{
    public class User
    {
        [Required, Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PreferredCurrency { get; set; }


    }
}
