using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не вказаний Email!")]
        [EmailAddress(ErrorMessage = "Email вказаний в неправильному форматі!")]
        [Display(Name = "Логін")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не вказаний пароль!")]
        [UIHint("Password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене?")]
        public bool RememberMe { get; set; }
    }
}
