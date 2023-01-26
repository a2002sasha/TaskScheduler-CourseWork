using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModel
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Не вказано ім'я!")]
        [Display(Name = "Ваше ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не вказане прізвище!")]
        [Display(Name = "Ваше прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не вказаний Email!")]
        [EmailAddress(ErrorMessage = "Email вказаний в неправильному форматі!")]
        [Display(Name = "Ваша електронна пошта")]
        [UIHint("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не вказаний пароль!")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Максимальна довжина пароля пароля - 128, мінімальна - 6")]
        [Display(Name = "Введіть пароль")]
        [UIHint("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не вказано підтвердження пароля!")]
        [Display(Name = "Підтвердіть введений пароль")]
        [UIHint("Password")]
        [Compare("Password", ErrorMessage = "Пароль введено неправильно!")]
        public string ConfirmPassword { get; set; }
    }
}
