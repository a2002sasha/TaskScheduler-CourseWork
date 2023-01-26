using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoList.DataAccess.Model;

namespace ToDoList.ViewModel
{
    public class UserTaskViewModel : UserTask
    {
        [Required(ErrorMessage = "Назва завдання не вказана!")]
        [Display(Name = "Вкажіть назву завдання")]
        public override string Name { get; set; }

        [Required(ErrorMessage = "Опис завдання не вказаний!")]
        [Display(Name = "Опишіть завдання")]
        public override string Description { get; set; }

        [Required(ErrorMessage = "Пріоритет завдання не вказаний!")]
        [Display(Name = "Пріоритет завдання")]
        public override TaskLevel TaskLevel { get; set; }

        [Required(ErrorMessage = "Статус завдання не вказаний!")]
        [Display(Name = "Статус завдання")]
        public override TaskState TaskState { get; set; }

        [Required(ErrorMessage = "Вкажіть дату виконання завдання!")]
        [Display(Name = "Дата")]
        [UIHint("Date")]
        public override string Date { get; set; }

        [Required(ErrorMessage = "Вкажіть час виконання завдання!")]
        [Display(Name = "Час")]
        [UIHint("Time")]
        public override string Time { get; set; }
        public IEnumerable<UserTask> UserTasks { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
