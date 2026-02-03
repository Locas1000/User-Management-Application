using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Task4.Server.Models
{
    // REQUIRED: Unique Index on Email enforced at storage level
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        // Required for sorting the table
        public DateTime LastLoginTime { get; set; }

        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;

        // Status can be: "Unverified", "Active", "Blocked"
        public string Status { get; set; } = "Unverified";
    }
}