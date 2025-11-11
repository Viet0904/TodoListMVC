using Microsoft.AspNetCore.Mvc;

namespace TodoListMVC.Models
{
    public class Tasks : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
