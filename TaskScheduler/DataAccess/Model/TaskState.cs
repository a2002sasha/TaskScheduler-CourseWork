﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList.DataAccess.Model
{
    public class TaskState
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
