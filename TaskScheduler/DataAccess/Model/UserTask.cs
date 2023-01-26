using System.ComponentModel.DataAnnotations;

namespace ToDoList.DataAccess.Model
{
    public class UserTask
    {
        [Key]
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public User User { get; set; }
        public virtual TaskLevel TaskLevel { get; set; }
        public virtual TaskState TaskState { get; set; }
        public virtual string Date { get; set; }
        public virtual string Time { get; set; }
    }
}
