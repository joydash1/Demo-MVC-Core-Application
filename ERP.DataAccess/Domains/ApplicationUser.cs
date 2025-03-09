using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class ApplicationUser
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Employee code is required.")]
        public required string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Mobile No is required.")]
        [MinLength(11, ErrorMessage = "Mobile No must be at least 11 digit.")]
        public required string MobileNo { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public required string Password { get; set; }
        public string? ProfilePicture { get; set; }
        public string? ProfilePicturePath { get; set; }
        public  bool? IsLoggedIn { get; set; }

        // JWT Token field
        public string? JwtToken { get; set; }

        // Refresh Token field
        public string? RefreshToken { get; set; }

        // Refresh Token Expiry Time
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
