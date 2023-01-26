using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.DataAccess.Model;

namespace ToDoList.DataAccess.Context
{
    public class ToDoContext : IdentityDbContext<User>
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskState> TaskStates { get; set; }
        public DbSet<TaskLevel> TaskLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskLevel>().HasData(new TaskLevel[]
            {
                new TaskLevel() {Id = 1, Name = "Низький"},
                new TaskLevel() {Id = 2, Name = "Середній"},
                new TaskLevel() {Id = 3, Name = "Високий"},
                new TaskLevel() {Id = 4, Name = "Критичний"}
            });

            builder.Entity<TaskState>().HasData(new TaskState[]
            {
                new TaskState() {Id = 1, Name = "Створено"},
                new TaskState() {Id = 2, Name = "В процесі"},
                new TaskState() {Id = 3, Name = "Завершено"},
            });

            base.OnModelCreating(builder);
        }
    }
}
