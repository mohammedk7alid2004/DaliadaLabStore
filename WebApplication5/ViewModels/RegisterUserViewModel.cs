using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels
{
    public class RegisterUserViewModel
    { 
        public string UserName { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name ="confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Email { get; set; } =string.Empty;
        public string PhoneNumber { get; set; }
        = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
