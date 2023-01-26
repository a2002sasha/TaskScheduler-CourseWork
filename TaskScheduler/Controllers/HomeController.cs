using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Configuration;

namespace ToDoList.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            StyleConfig.UseBootstrap = false;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
