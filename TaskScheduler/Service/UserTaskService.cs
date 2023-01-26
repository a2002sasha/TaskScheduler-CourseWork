using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.DataAccess.Context;
using ToDoList.DataAccess.Model;
using ToDoList.Enums;

namespace ToDoList.Service
{
    public class UserTaskService : IUserTaskService
    {
        private readonly ToDoContext _context;

        public UserTaskService(ToDoContext context)
        {
            _context = context;
        }


        public IQueryable<UserTask> GetAllTasks()
        {
            return _context.UserTasks
                .Include(u => u.User)
                .Include(u => u.TaskLevel)
                .Include(u => u.TaskState)
                .AsQueryable();
        }

        public IQueryable<UserTask> GetTasksByUser(string username)
        {
            return _context.UserTasks
                .Where(u => u.User.UserName == username)
                .Include(u => u.User)
                .Include(u => u.TaskLevel)
                .Include(u => u.TaskState)
                .AsQueryable();
        }

        public IQueryable<TaskLevel> GetTaskLevels()
        {
            return _context.TaskLevels.AsQueryable();
        }

        public void Create(UserTask task, string username)
        {

            var user = _context.Users.First(t => t.UserName == username);

            var taskLevelContext = _context.TaskLevels.First(tl => tl.Name == task.TaskLevel.Name);
            var taskStateContext = _context.TaskStates.First(ts => ts.Name == task.TaskState.Name);

            _context.UserTasks.Add(new UserTask()
            {
                User = user,
                Name = task.Name,
                Description = task.Description,
                Date = task.Date,
                Time = task.Time,
                TaskLevel = taskLevelContext,
                TaskState = taskStateContext,
            });

            _context.SaveChanges();
        }

        public void Edit(UserTask task)
        {
            var userTask = _context.UserTasks
                    .Include(ut => ut.TaskState)
                    .Include(ut => ut.TaskLevel)
                    .First(ut => ut.Id == task.Id);

            userTask.Name = task.Name;
            userTask.Description = task.Description;
            userTask.Date = task.Date;
            userTask.Time = task.Time;
            userTask.TaskLevel = _context.TaskLevels.First(tl => tl.Name == task.TaskLevel.Name);
            userTask.TaskState = _context.TaskStates.First(ts => ts.Name == task.TaskState.Name);

            _context.SaveChanges();
        }

        public void Remove(int taskId)
        {
            var task = _context.UserTasks.First(t => t.Id == taskId);
            _context.Remove(task);

            _context.SaveChanges();
        }

        public string[] GetTaskLevelNames()
        {
            return _context.TaskLevels.Select(t => t.Name).ToArray();
        }

        public string[] GetTaskStateNames()
        {
            return _context.TaskStates.Select(t => t.Name).ToArray();
        }

        public List<UserTask> GetSortedAndFilteredUserTasks(string username, int? level, string name, SortState sortOrder = SortState.NameAsc)
        {
            // Фільтрація
            var userTasks = GetTasksByUser(username);

            if (level != null && level != 0)
            {
                userTasks = userTasks.Where(p => p.TaskLevel.Id == level);
            }
            if (!String.IsNullOrEmpty(name))
            {
                userTasks = userTasks.Where(p => p.Name.Contains(name));
            }

            // Сортування
            userTasks = sortOrder switch
            {
                SortState.NameDesc => userTasks.OrderByDescending(s => s.Name),
                SortState.DescriptionAsc => userTasks.OrderBy(s => s.Description),
                SortState.DescriptionDesc => userTasks.OrderByDescending(s => s.Description),
                SortState.TaskLevelAsc => userTasks.OrderBy(s => s.TaskLevel.Name),
                SortState.TaskLevelDesc => userTasks.OrderByDescending(s => s.TaskLevel.Name),
                SortState.TaskStateAsc => userTasks.OrderBy(s => s.TaskState.Name),
                SortState.TaskStateDesc => userTasks.OrderByDescending(s => s.TaskState.Name),
                SortState.DateAsc => userTasks.OrderBy(s => s.Date),
                SortState.DateDesc => userTasks.OrderByDescending(s => s.Date),
                SortState.TimeAsc => userTasks.OrderBy(s => s.Time),
                SortState.TimeDesc => userTasks.OrderByDescending(s => s.Time),
                _ => userTasks.OrderBy(s => s.Name)
            };

            var sortedAndFilteredUserTasks = userTasks.ToList();

            return sortedAndFilteredUserTasks;
        }

        public List<Chart> GetDataForChartList(string username)
        {
            var userTasks = GetTasksByUser(username);
            var allTaskLevels = GetTaskLevels();

            List<int> countOfTaskLevels = new List<int>();
            var taskLevelsName = allTaskLevels.Select(x => x.Name).ToList();
            var taskLevelsId = allTaskLevels.Select(x => x.Id).Distinct();

            foreach (int item in taskLevelsId)
            {
                countOfTaskLevels.Add(userTasks.Count(x => x.TaskLevel.Id == item));
            }


            var dataForChartlist = new List<Chart>();
            dataForChartlist.Add(new Chart() { TaskLevelName = taskLevelsName[0], CountOfTaskLevels = countOfTaskLevels[0] });
            dataForChartlist.Add(new Chart() { TaskLevelName = taskLevelsName[1], CountOfTaskLevels = countOfTaskLevels[1] });
            dataForChartlist.Add(new Chart() { TaskLevelName = taskLevelsName[2], CountOfTaskLevels = countOfTaskLevels[2] });
            dataForChartlist.Add(new Chart() { TaskLevelName = taskLevelsName[3], CountOfTaskLevels = countOfTaskLevels[3] });


            return dataForChartlist;
        }
    }
}
