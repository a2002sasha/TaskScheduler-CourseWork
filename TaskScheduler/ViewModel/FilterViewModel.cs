using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.DataAccess.Model;

namespace ToDoList.ViewModel
{
    public class FilterViewModel
    {
        public FilterViewModel(List<TaskLevel> taskLevels, int? level, string name)
        {
            taskLevels.Insert(0, new TaskLevel() { Name = "Усі пріоритети", Id = 0 });
            TaskLevels = new SelectList(taskLevels, "Id", "Name", level);
            SelectedLevel = level;
            SelectedName = name;
        }

        public SelectList TaskLevels { get; private set; }
        public int? SelectedLevel { get; private set; }
        public string SelectedName { get; private set; }
    }
}
