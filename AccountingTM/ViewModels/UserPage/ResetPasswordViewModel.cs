using System.ComponentModel.DataAnnotations;

namespace AccountingTM.ViewModels.UserPage
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Новый пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}