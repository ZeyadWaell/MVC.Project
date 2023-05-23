using System.ComponentModel.DataAnnotations;

namespace Demo.NL.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [MinLength(5,ErrorMessage ="Mininm length is 5")]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool isAgree { get; set; }
    }
}
