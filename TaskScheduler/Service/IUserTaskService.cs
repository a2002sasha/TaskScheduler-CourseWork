using System.Collections.Generic;
using System.Linq;
using ToDoList.DataAccess.Model;
using ToDoList.Enums;

namespace ToDoList.Service
{
    public interface IUserTaskService
    {
        IQueryable<UserTask> GetAllTasks();
        IQueryable<UserTask> GetTasksByUser(string username);
        IQueryable<TaskLevel> GetTaskLevels();
        void Create(UserTask task, string username);
        void Edit(UserTask task);
        void Remove(int taskId);
        string[] GetTaskLevelNames();
        string[] GetTaskStateNames();
        List<UserTask> GetSortedAndFilteredUserTasks(string username, int? level, string name, SortState sortOrder = SortState.NameAsc);
        List<Chart> GetDataForChartList(string username);
    }
}
