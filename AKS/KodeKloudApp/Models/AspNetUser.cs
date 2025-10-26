using System.ComponentModel.DataAnnotations;

namespace KodeKloudApp.Models
{
    public class AspNetUser
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public string CountryCode { get; set; } = string.Empty;
        
        [Required]
        public string NationalNumber { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? LastLoginAt { get; set; }
        
        public string? UserName { get; set; }
        
        public string? NormalizedUserName { get; set; }
        
        public string? Email { get; set; }
        
        public string? NormalizedEmail { get; set; }
        
        public bool EmailConfirmed { get; set; } = false;
        
        public string? PasswordHash { get; set; }
        
        public string? SecurityStamp { get; set; }
        
        public string? ConcurrencyStamp { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; } = false;
        
        public bool TwoFactorEnabled { get; set; } = false;
        
        public DateTime? LockoutEnd { get; set; }
        
        public bool LockoutEnabled { get; set; } = true;
        
        public int AccessFailedCount { get; set; } = 0;
    }
}
