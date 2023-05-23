using System.ComponentModel.DataAnnotations;

namespace Demo.NL.Models.Account
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
