using AhmedovTravel.Infrastructure.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace AhmedovTravel.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserConstants.FirstNameMaxLength, MinimumLength = UserConstants.FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.LastNameMaxLength, MinimumLength = UserConstants.LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.UserNameMaxLength, MinimumLength = UserConstants.UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(UserConstants.EmailMaxLength, MinimumLength = UserConstants.EmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.PasswordMaxLength, MinimumLength = UserConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
