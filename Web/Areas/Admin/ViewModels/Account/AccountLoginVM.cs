using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
