using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class AccountRegisterVM
    {
        [Required, MaxLength(100)]
        public string Fullname { get; set; }

        [Required, MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, RegularExpression("(?=^.{8,}$)((?=.*\\d)(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Your password must contain Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(100), DataType(DataType.Password), Display(Name = "Confirm Password"), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
