using System.ComponentModel.DataAnnotations;

namespace Demo.NL.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Mininm length is 5")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
