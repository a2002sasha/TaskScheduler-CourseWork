using System.ComponentModel.DataAnnotations;

namespace ToDoList.DataAccess.Model
{
    public class TaskLevel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
