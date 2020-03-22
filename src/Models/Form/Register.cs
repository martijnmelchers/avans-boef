using System.ComponentModel.DataAnnotations;

namespace Models.Form
{
    public class Register
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Herhaal wachtwoord")]
        public string PasswordRepeat { get; set; }
        public bool IsAdmin { get; set; }
    }
}