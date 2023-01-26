using Microsoft.AspNetCore.Mvc;
using ToDoList.Configuration;
using ToDoList.Service;

namespace ToDoList.Controllers
{
    public class ChartController : Controller
    {
        private readonly IUserTaskService _taskService;

        public ChartController(IUserTaskService taskService)
        {
            _taskService = taskService;
            StyleConfig.UseBootstrap = false;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult TaskLevelsStatisticsChart()
        {
            var username = HttpContext.User.Identity?.Name;
            var dataForChartList = _taskService.GetDataForChartList(username);

            return Json(dataForChartList);
        }
    }
}
