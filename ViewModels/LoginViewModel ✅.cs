using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ایمیل الزامی است.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
        [Required]
        public string? Captcha { get; set; }
    }
}