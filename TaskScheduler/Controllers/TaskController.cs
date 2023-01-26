using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDoList.Configuration;
using ToDoList.DataAccess.Model;
using ToDoList.Enums;
using ToDoList.Service;
using ToDoList.ViewModel;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IUserTaskService _taskService;

        public TaskController(IUserTaskService taskService)
        {
            _taskService = taskService;
            StyleConfig.UseBootstrap = false;
        }

        [HttpGet]
        public IActionResult Index(int? level, string name, SortState sortOrder = SortState.NameAsc)
        {
            var username = HttpContext.User.Identity?.Name;

            var sortedAndFilteredUserTasks = _taskService.GetSortedAndFilteredUserTasks(username, level, name, sortOrder);

            UserTaskViewModel viewModel = new UserTaskViewModel()
            {
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_taskService.GetTaskLevels().ToList(), level, name),
                UserTasks = sortedAndFilteredUserTasks
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new UserTaskViewModel();

            var taskLevels = _taskService.GetTaskLevelNames();
            SelectList levelsList = new SelectList(taskLevels, taskLevels[0]);

            var taskStates = _taskService.GetTaskStateNames();
            SelectList stateList = new SelectList(taskStates, taskStates[0]);

            ViewBag.taskLevels = levelsList;
            ViewBag.taskStates = stateList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(UserTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = new UserTask()
                {
                    Id = model.Id,
                    Date = model.Date,
                    Time = model.Time,
                    Description = model.Description,
                    Name = model.Name,
                    TaskLevel = model.TaskLevel,
                    TaskState = model.TaskState
                };

                var username = HttpContext.User.Identity?.Name;
                _taskService.Create(task, username);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTask = _taskService.GetAllTasks().First(t => t.Id == id);

            if (userTask == null)
            {
                return NotFound();
            }

            var model = new UserTaskViewModel()
            {
                Id = userTask.Id,
                Date = userTask.Date,
                Time = userTask.Time,
                Description = userTask.Description,
                Name = userTask.Name,
                TaskLevel = userTask.TaskLevel,
                TaskState = userTask.TaskState
            };

            var taskLevels = _taskService.GetTaskLevelNames();
            SelectList levelsList = new SelectList(taskLevels, userTask.TaskLevel.Name);

            var taskStates = _taskService.GetTaskStateNames();
            SelectList stateList = new SelectList(taskStates, userTask.TaskState.Name);

            ViewBag.taskLevels = levelsList;
            ViewBag.taskStates = stateList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, UserTaskViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _taskService.Edit(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var tasks = _taskService.GetAllTasks().Any(t => t.Id == model.Id);

                    if (!tasks)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTask = _taskService.GetAllTasks().First(t => t.Id == id);

            if (userTask == null)
            {
                return NotFound();
            }

            var model = new UserTaskViewModel()
            {
                Id = userTask.Id,
                Date = userTask.Date,
                Time = userTask.Time,
                Description = userTask.Description,
                Name = userTask.Name,
                TaskLevel = userTask.TaskLevel,
                TaskState = userTask.TaskState
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _taskService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
